using MediatR;
using RentalCar.Service.Application.Commands.Response;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Commands.Request;
public class DeleteServiceRequest(string id) : IRequest<ApiResponse<InputServiceResponse>>
{
    public string Id { get; set; } = id;
}

