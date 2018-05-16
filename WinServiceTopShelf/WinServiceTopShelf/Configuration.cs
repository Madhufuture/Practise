namespace WinServiceTopShelf
{
    using Topshelf;

    internal static class Configuration
    {
        internal static void Configure()
        {
            HostFactory.Run(config => config.Service<Service>(service =>
            {
                service.ConstructUsing(s => new Service());
                service.WhenStarted(s => s.Start());
                service.WhenStopped(s => s.Stop());
            }));

            HostFactory.New(config =>
            {
                config.EnableServiceRecovery(x =>
                {
                    x.OnCrashOnly();
                    x.RestartService(0);
                });
            });
        }
    }
}
