using MediatR;
using RentalCar.Service.Application.Commands.Response;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Commands.Request;
public class UpdateServiceRequest : IRequest<ApiResponse<InputServiceResponse>>
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

