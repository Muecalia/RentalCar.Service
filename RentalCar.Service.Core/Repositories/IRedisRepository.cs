using RentalCar.Service.Core.Entities;

namespace RentalCar.Service.Core.Repositories;

public interface IRedisRepository
{
    Task CreateService<T>(string key, T value);
    Task<string> GetService<T>(string key);
}