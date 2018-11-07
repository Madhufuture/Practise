namespace ProductCatalog.API.Logger
{
    public interface ILoggerAdapter<T>
    {
        void LogInformation(string message);
        void LogError(string message, params object[] args);

    }
}