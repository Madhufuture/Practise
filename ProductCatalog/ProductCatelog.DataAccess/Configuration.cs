namespace ProductCatalog.DataAccess
{
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public static class Configuration
    {
        public static IConfiguration Config { get; set; }

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true).AddEnvironmentVariables();
            Config = builder.Build();
        }

        public static string GetConnString()
        {
            if (Config == null)
            {
                Initialize();
            }
            var connString = $"{Config["ConnectionString:LocalDBConnectionString"]}";

            return connString;

        }
    }
}