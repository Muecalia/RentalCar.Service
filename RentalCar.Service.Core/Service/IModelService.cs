namespace RentalCar.Service.Core.Service;

public interface IModelService
{
    Task<string> GetService(string idCategory, string queue, CancellationToken cancellationToken);
    //Task<string> GetCategory(string idCategory, CancellationToken cancellationToken);
    //Task<string> GetManufacturer(string idManufacturer, CancellationToken cancellationToken);
}