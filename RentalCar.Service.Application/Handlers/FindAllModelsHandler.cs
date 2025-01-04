using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Service.Application.Queries.Request;
using RentalCar.Service.Application.Queries.Response;
using RentalCar.Service.Core.Configs;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;
using RentalCar.Service.Core.Wrappers;

namespace RentalCar.Service.Application.Handlers;

public class FindAllServicesHandler : IRequestHandler<FindAllServicesRequest, PagedResponse<FindServiceResponse>>
{
    private readonly IServiceRepository _repository;
    private readonly ILoggerService _loggerService;
    private readonly IPrometheusService _prometheusService;

    public FindAllServicesHandler(IServiceRepository repository, ILoggerService loggerService, IPrometheusService prometheusService)
    {
        _repository = repository;
        _loggerService = loggerService;
        _prometheusService = prometheusService;
    }

    public async Task<PagedResponse<FindServiceResponse>> Handle(FindAllServicesRequest request, CancellationToken cancellationToken)
    {
        const string Objecto = "serviços";
        try
        {
            var services = await _repository.GetAll(request.PageNumber, request.PageSize, cancellationToken);
            _prometheusService.AddFindAllServicesCounter(StatusCodes.Status200OK.ToString());
            var results = services.Select(service => new FindServiceResponse(service.Id, service.Name, service.Description, 
                service.CreatedAt.ToShortDateString(), service.UpdatedAt?.ToShortDateString())).ToList(); 
            
            return new PagedResponse<FindServiceResponse>(results, request.PageNumber, request.PageSize, results.Count, MessageError.CarregamentoSucesso(Objecto, results.Count));
        }
        catch (Exception ex)
        {
            _prometheusService.AddFindAllServicesCounter(StatusCodes.Status500InternalServerError.ToString());
            _loggerService.LogError(MessageError.CarregamentoErro(Objecto, ex.Message));
            return new PagedResponse<FindServiceResponse>(MessageError.CarregamentoErro(Objecto));
            //throw;
        }
    }

}
