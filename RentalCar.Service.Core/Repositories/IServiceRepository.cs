using RentalCar.Service.Core.Entities;

namespace RentalCar.Service.Core.Repositories;

public interface IServiceRepository
{
    Task<Services> Create(Services service, CancellationToken cancellationToken);
    Task Update(Services service, CancellationToken cancellationToken);
    Task Delete(Services service, CancellationToken cancellationToken);
    Task<bool> IsServiceExist(string name, CancellationToken cancellationToken);
    Task<Services?> GetById(string id, CancellationToken cancellationToken);
    Task<List<Services>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
}