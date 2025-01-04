using MediatR;
using RentalCar.Service.Core.Wrappers;
using RentalCar.Service.Application.Queries.Response;

namespace RentalCar.Service.Application.Queries.Request
{
    public class FindServiceByIdRequest(string id) : IRequest<ApiResponse<FindServiceResponse>>
    {
        public string Id { get; set; } = id;
    }
}
