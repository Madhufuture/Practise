namespace ProductCatalog.API.Logger
{
    using Microsoft.Extensions.Logging;

    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }
    }
}