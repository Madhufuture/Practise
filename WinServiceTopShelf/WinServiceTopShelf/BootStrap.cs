using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using Owin;
using Hangfire;

namespace WinServiceTopShelf
{
    public class BootStrap
    {
        private IDisposable _host;

        /// <summary>
        /// Configure owin hosting options for the hangfire
        /// </summary>
        public void Start()
        {
            var options = new StartOptions {Port = 8999};
            _host = WebApp.Start<Startup>(options);
            Console.WriteLine();
            Console.WriteLine("HangFire has started");
            Console.WriteLine("Dashboard is available at http://localhost:8999/hangfire");
            Console.WriteLine();
        }

        public void Stop()
        {
            _host.Dispose();
        }
    }

    public class Startup
    {
        /// <summary>
        /// Configure Hangfire Connection string, dashboard
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager
                .ConnectionStrings["HangFireConnectionString"].ConnectionString);
            appBuilder.UseHangfireDashboard();
            appBuilder.UseHangfireServer();

            var jobSvc = new HangFireService();
            jobSvc.ScheduleJobs();
        }
    }
}