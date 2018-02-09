using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;

namespace TableAndBlobStorage
{
    public class Program
    {
        static void Main()
        {
            var fullPath = Path.GetFullPath(Path.Combine(@"../../configurationSettings.json"));
            ReadJsonData(fullPath);
        }

        public static void ReadJsonData(string file)
        {
            const string delimitter = ":";
            string json;
            using (var reader = new StreamReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                json = reader.ReadToEnd();
            }

            var token = JToken.Parse(json);
            var tokenValues = token.Select(val => Convert.ToString(val).Split(delimitter.ToCharArray(), 2));
            GetPartitionKeyAndData(tokenValues);

        }

        private static void GetPartitionKeyAndData(IEnumerable<string[]> tokenValues)
        {
            Initialize(out var table, out var blobContainer);
            var blobName = string.Concat("integrationsettings", DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss"));

            foreach (var value in tokenValues)
            {
                var partitionKey = value[0].Replace("\"", string.Empty);
                var integrationConfigValues = Encoding.Unicode.GetBytes(value[1]);

                //InsertData(table, blobContainer, blobName, partitionKey, integrationConfigValues);
                ReadData(partitionKey, partitionKey);
            }
        }

        private static void InsertData(CloudTable table, CloudBlobContainer blobContainer, string blobName, string partitionKey, byte[] integrationConfigValues)
        {
            var settings = new Settings(partitionKey);
            blobContainer.CreateIfNotExists();
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            using (Stream data = new MemoryStream(integrationConfigValues))
            {
                blockBlob.UploadFromStream(data);
            }

            var uri = blockBlob.Uri;
            settings.BlobUri = uri.AbsoluteUri;

            var isTableExists = table.Exists();
            if (isTableExists)
            {
                var retreiveData = TableOperation.Retrieve<Settings>(settings.PartitionKey, settings.RowKey);
                var retreiveResult = table.Execute(retreiveData);
                var data = retreiveResult.Result;

                if (data != null)
                {
                    retreiveResult.Etag = "*";
                    var updateData = TableOperation.Replace(settings);
                    var updateResult = table.Execute(updateData);
                }
            }
            else
            {
                var insertData = TableOperation.Insert(settings);
                table.CreateIfNotExists();
                var result = table.Execute(insertData);
            }
        }

        private static void Initialize(out CloudTable table, out CloudBlobContainer blobContainer)
        {
            var storageAccountDetails =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("storageConnectionString"));
            CloudTableClient tableClient = storageAccountDetails.CreateCloudTableClient();
            CloudBlobClient blobClient = storageAccountDetails.CreateCloudBlobClient();

            table = tableClient.GetTableReference("integrationsettings");
            blobContainer = blobClient.GetContainerReference("integrationsettings");
        }

        private static void ReadData(string partitionKey, string keyName)
        {
            Initialize(out var table, out var blobContainer);

            if (table.Exists())
            {
                TableOperation tableOperation = TableOperation.Retrieve<Settings>(partitionKey, keyName);
                var result = table.Execute(tableOperation);
                var data = result.Result;

                if (data is Settings settingsData)
                {
                    var settings = new Settings
                    {
                        PartitionKey = settingsData.PartitionKey,
                        RowKey = settingsData.RowKey,
                        BlobUri = settingsData.BlobUri
                    };

                    var blob = blobContainer.ServiceClient.GetBlobReferenceFromServer(new Uri(settings.BlobUri));

                    using (var sm = new MemoryStream())
                    {
                        blob.DownloadToStream(sm);
                        var blobByteData = sm.ToArray();
                        var actualBlobData = Encoding.Unicode.GetString(blobByteData);
                    }
                }
            }
        }
    }

    public class Settings : TableEntity
    {
        public Settings(string partitionKey)
        {
            PartitionKey = partitionKey;
            RowKey = partitionKey;
            ETag = "*";
        }
        public Settings() { }
        public string BlobUri { get; set; }
    }
}


