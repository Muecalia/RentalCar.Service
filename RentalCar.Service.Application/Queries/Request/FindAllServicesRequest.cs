using MediatR;
using RentalCar.Service.Application.Queries.Response;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Queries.Request;

public class FindAllServicesRequest(int pageNumber, int pageSize) : IRequest<PagedResponse<FindServiceResponse>>
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}

