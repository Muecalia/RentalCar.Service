using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Service.Application.Commands.Request;
using RentalCar.Service.Application.Commands.Response;
using RentalCar.Service.Core.Configs;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Handlers;

public class DeleteServiceHandler : IRequestHandler<DeleteServiceRequest, ApiResponse<InputServiceResponse>>
{
    private readonly IServiceRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public DeleteServiceHandler(IServiceRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<ApiResponse<InputServiceResponse>> Handle(DeleteServiceRequest request, CancellationToken cancellationToken)
    {
        const string Objecto = "serviço";
        const string Operacao = "eliminar";
        try
        {
            var service = await _repository.GetById(request.Id, cancellationToken);
            if (service == null)
            {
                _loggerService.LogWarning(MessageError.NotFound(Objecto, request.Id));
                _prometheusService.AddDeleteServiceCounter(StatusCodes.Status404NotFound.ToString());
                return ApiResponse<InputServiceResponse>.Error(MessageError.NotFound(Objecto));
            }

            await _repository.Delete(service, cancellationToken);
            var result = new InputServiceResponse(service.Id, service.Name, service.DeletedAt?.ToShortDateString());
            //var result = new InputServiceResponse(Service.Id, Service.Name);
            _prometheusService.AddDeleteServiceCounter(StatusCodes.Status204NoContent.ToString());
            return ApiResponse<InputServiceResponse>.Success(result, MessageError.OperacaoSucesso(Objecto, Operacao));
        }
        catch (Exception ex)
        {
            _prometheusService.AddDeleteServiceCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.OperacaoErro(Objecto, Operacao, ex.Message));
            return ApiResponse<InputServiceResponse>.Error(MessageError.OperacaoErro(Objecto, Operacao));
            //throw;
        }
    }
}

