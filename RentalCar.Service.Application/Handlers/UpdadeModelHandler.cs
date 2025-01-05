using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Service.Application.Commands.Request;
using RentalCar.Service.Application.Commands.Response;
using RentalCar.Service.Core.Configs;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Handlers;

public class UpdadeServiceHandler : IRequestHandler<UpdateServiceRequest, ApiResponse<InputServiceResponse>>
{
    private readonly IServiceRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public UpdadeServiceHandler(IServiceRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<ApiResponse<InputServiceResponse>> Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
    {
        const string Objecto = "serviço";
        const string Operacao = "editar";
        try
        {
            var service = await _repository.GetById(request.Id, cancellationToken);
            if (service == null)
            {
                _prometheusService.AddUpdateServiceCounter(StatusCodes.Status404NotFound.ToString());
                _loggerService.LogWarning(MessageError.NotFound(Objecto, request.Id));
                return ApiResponse<InputServiceResponse>.Error(MessageError.NotFound(Objecto));
            }

            service.Name = request.Name;
            service.Description = request.Description;

            await _repository.Update(service, cancellationToken);
            
            _prometheusService.AddUpdateServiceCounter(StatusCodes.Status200OK.ToString());
            var result = new InputServiceResponse(service.Id, service.Name, service.UpdatedAt?.ToShortDateString());
            return ApiResponse<InputServiceResponse>.Success(result, MessageError.OperacaoSucesso(Objecto, Operacao));
        }
        catch (Exception ex)
        {
            _prometheusService.AddUpdateServiceCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.OperacaoErro(Objecto, Operacao, ex.Message));
            return ApiResponse<InputServiceResponse>.Error(MessageError.OperacaoErro(Objecto, Operacao));
            //throw;
        }
    }
}

