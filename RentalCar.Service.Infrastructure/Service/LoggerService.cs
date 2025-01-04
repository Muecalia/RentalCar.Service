using RentalCar.Service.Core.Service;
using Serilog;

namespace RentalCar.Service.Infrastructure.Service;

public class LoggerService : ILoggerService
{
    public LoggerService() { }

    public void LogError(string message)
    {
        Log.Error(message);
    }

    public void LogError(string message, Exception exception)
    {
        Log.Error(exception, message);
    }

    public void LogInformation(string message)
    {
        Log.Information(message);
    }

    public void LogWarning(string message)
    {
        Log.Warning(message);
    }
    
}