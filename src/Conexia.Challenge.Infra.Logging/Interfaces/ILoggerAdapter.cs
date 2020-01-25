using System;

namespace Conexia.Challenge.Infra.Logging.Interfaces
{
    public interface ILoggerAdapter<T>
    {
        void LogInformation(string message);
        void LogError(Exception ex, string message, params object[] args);
    }
}