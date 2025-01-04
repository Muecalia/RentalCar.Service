using Microsoft.EntityFrameworkCore;
using RentalCar.Service.Core.Entities;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Infrastructure.Persistence;

namespace RentalCar.Service.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly ServiceContext _context;

    public ServiceRepository(ServiceContext context)
    {
        _context = context;
    }

    public async Task<Services> Create(Services service, CancellationToken cancellationToken)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync(cancellationToken);
        return service;
    }

    public async Task Delete(Services service, CancellationToken cancellationToken)
    {
        service.DeletedAt = DateTime.UtcNow;
        service.IsDeleted = true;
        _context.Update(service);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Services>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Services
            .AsNoTracking()
            .Where(c => !c.IsDeleted)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Services?> GetById(string id, CancellationToken cancellationToken)
    {
        return await _context.Services.FirstOrDefaultAsync(c => !c.IsDeleted && string.Equals(c.Id, id), cancellationToken);
    }

    public async Task<bool> IsServiceExist(string name, CancellationToken cancellationToken)
    {
        return await _context.Services.AnyAsync(c => string.Equals(c.Name, name), cancellationToken);
    }

    public async Task Update(Services service, CancellationToken cancellationToken)
    {
        service.UpdatedAt = DateTime.UtcNow;
        _context.Services.Update(service);
        await _context.SaveChangesAsync(cancellationToken);
    }
}