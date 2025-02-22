namespace RentalCar.Service.Core.Service;

public interface ILoggerService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogError(string message, Exception exception);
}