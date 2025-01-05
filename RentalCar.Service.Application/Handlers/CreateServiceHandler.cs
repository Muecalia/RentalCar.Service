using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Service.Application.Commands.Request;
using RentalCar.Service.Application.Commands.Response;
using RentalCar.Service.Core.Configs;
using RentalCar.Service.Core.Entities;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Handlers;

public class CreateServiceHandler : IRequestHandler<CreateServiceRequest, ApiResponse<InputServiceResponse>>
{
    private readonly IServiceRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public CreateServiceHandler(IServiceRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<ApiResponse<InputServiceResponse>> Handle(CreateServiceRequest request, CancellationToken cancellationToken)
    {
        const string Objecto = "serviço";
        const string Operacao = "registo";
        try
        {
            if (await _repository.IsServiceExist(request.Name, cancellationToken))
            {
                _loggerService.LogWarning(MessageError.Conflito($"{Objecto} {request.Name}"));
                _prometheusService.AddNewServiceCounter(StatusCodes.Status409Conflict.ToString());
                return ApiResponse<InputServiceResponse>.Error(MessageError.Conflito(Objecto));
            }

            var newService = new Services
            {
                Name = request.Name,
                Description = request.Description
            };

            var service = await _repository.Create(newService, cancellationToken);

            var result = new InputServiceResponse(service.Id, service.Name, service.CreatedAt.ToShortDateString());

            _prometheusService.AddNewServiceCounter(StatusCodes.Status201Created.ToString());
            return ApiResponse<InputServiceResponse>.Success(result, MessageError.OperacaoProcessamento(Objecto, Operacao));
        }
        catch (Exception ex)
        {
            _prometheusService.AddNewServiceCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.OperacaoErro(Objecto, Operacao, ex.Message));
            return ApiResponse<InputServiceResponse>.Error(MessageError.OperacaoErro(Objecto, Operacao));
        }
    }
}
