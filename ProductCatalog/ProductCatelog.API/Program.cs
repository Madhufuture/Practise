namespace ProductCatalog.API
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).UseSerilog().Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}