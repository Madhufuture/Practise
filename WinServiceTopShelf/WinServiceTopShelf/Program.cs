using Topshelf;

namespace WinServiceTopShelf
{
    public class Program
    {
        /// <summary>
        /// Configure TopShelf to construct service using HangFire
        /// Bootstrap class - has Hangfire configuration
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            HostFactory.Run(config =>
            {
                config.Service<BootStrap>(service =>
                {
                    service.ConstructUsing(s => new BootStrap());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                config.RunAsLocalSystem();
                config.SetDescription("TopShelf Service");
                config.SetDisplayName("TopShelfService");

            });
        }
    }
}


//HostFactory.New(config =>
//{
//config.EnableServiceRecovery(x =>
//{
//    x.OnCrashOnly();
//    x.RestartService(0);
//});
//});