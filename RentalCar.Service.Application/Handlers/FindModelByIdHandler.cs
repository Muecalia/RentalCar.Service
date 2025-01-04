using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Service.Application.Queries.Request;
using RentalCar.Service.Application.Queries.Response;
using RentalCar.Service.Core.Configs;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Handlers;

public class FindServiceByIdHandler : IRequestHandler<FindServiceByIdRequest, ApiResponse<FindServiceResponse>>
{
    private readonly IServiceRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public FindServiceByIdHandler(IServiceRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<ApiResponse<FindServiceResponse>> Handle(FindServiceByIdRequest request, CancellationToken cancellationToken)
    {
        const string Objecto = "serviço";
        try
        {
            var service = await _repository.GetById(request.Id, cancellationToken);
            if (service is null)
            {
                _loggerService.LogWarning(MessageError.NotFound(Objecto, request.Id));
                _prometheusService.AddFindByIdServiceCounter(StatusCodes.Status404NotFound.ToString());
                return ApiResponse<FindServiceResponse>.Error(MessageError.NotFound(Objecto));
            }

            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            var result = new FindServiceResponse(service.Id, service.Name, service.Description, service.CreatedAt.ToShortDateString(), service.UpdatedAt?.ToShortDateString());
            
            _prometheusService.AddFindByIdServiceCounter(StatusCodes.Status200OK.ToString());
            _loggerService.LogInformation(MessageError.CarregamentoSucesso(Objecto, 1));
            return ApiResponse<FindServiceResponse>.Success(result, MessageError.CarregamentoSucesso(Objecto));
        }
        catch (Exception ex)
        {
            _prometheusService.AddFindByIdServiceCounter(StatusCodes.Status400BadRequest.ToString());
            _loggerService.LogError(MessageError.CarregamentoErro(Objecto, ex.Message));
            return ApiResponse<FindServiceResponse>.Error(MessageError.CarregamentoErro(Objecto));
        }
    }
}

