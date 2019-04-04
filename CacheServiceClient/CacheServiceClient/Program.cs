using CacheService.Contracts;
using Microsoft.ServiceBus;
using System;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Text;

namespace CacheService.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            var value = Convert.ToInt32(args[0]);

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "AzureSqlserver";
            builder.UserID = "userid";
            builder.Password = "pwd";
            builder.InitialCatalog = "database";

            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.AutoDetect;
            var cacheServiceChannel = new ChannelFactory<ICacheServiceChannel>("CacheService");

            var response = new StudentDetails();
            ICacheServiceChannel channel = null;

            try
            {
                channel = cacheServiceChannel.CreateChannel();
                response = channel.GetStudentDetails(value);

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append($"Insert into Details(Id,Name,Age) values ({response.Id},'{response.Name}',{response.Age})");

                    using(SqlCommand cmd = new SqlCommand(sb.ToString(), connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
