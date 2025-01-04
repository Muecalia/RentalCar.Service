using MediatR;
using RentalCar.Service.Application.Commands.Response;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Commands.Request;
public class CreateServiceRequest : IRequest<ApiResponse<InputServiceResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
}

