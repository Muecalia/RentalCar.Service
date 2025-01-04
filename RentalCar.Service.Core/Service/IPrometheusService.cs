namespace RentalCar.Service.Core.Service;

public interface IPrometheusService
{
    void AddNewServiceCounter(string statusCodes);
    void AddDeleteServiceCounter(string statusCodes);
    void AddUpdateServiceCounter(string statusCodes);
    void AddUpdateStatusServiceCounter(string statusCodes);
    void AddFindByIdServiceCounter(string statusCodes);
    void AddFindAllServicesCounter(string statusCodes);
}