using MediatR;
using Microsoft.AspNetCore.Authorization;
using RentalCar.Service.Application.Commands.Request;
using RentalCar.Service.Application.Queries.Request;

namespace RentalCar.Service.API.EndPoints;

public static class ServiceEndPoint
{
    public static void MapServiceEndpoints(this IEndpointRouteBuilder builder)
    {
        // Get All
        builder.MapGet("/service", [Authorize(Roles = "Admin")] async (IMediator mediator, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10) =>
        {
            var result = await mediator.Send(new FindAllServicesRequest(pageNumber, pageSize), cancellationToken);
            
            return Results.Ok(result);
        });
        
        // Get By Id
        builder.MapGet("/service/{id}", [Authorize(Roles = "Admin")] async (IMediator mediator, CancellationToken cancellationToken, string id) =>
        {
            var result = await mediator.Send(new FindServiceByIdRequest(id), cancellationToken);
            
            return result.Succeeded ? Results.Ok(result) : Results.NotFound(result.Message);
        });
        
        // Create
        builder.MapPost("/service", [Authorize(Roles = "Admin")] async (IMediator mediator, CancellationToken cancellationToken, CreateServiceRequest request) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            
            return result.Succeeded ? Results.Created("", result) : Results.BadRequest(result.Message);
        });
        
        // Delete
        builder.MapDelete("/service/{id}", [Authorize(Roles = "Admin")] async (IMediator mediator, CancellationToken cancellationToken, string id) =>
        {
            var result = await mediator.Send(new DeleteServiceRequest(id), cancellationToken);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
        });
        
        // Update
        builder.MapPut("/service/{id}", [Authorize(Roles = "Admin")] async (IMediator mediator, CancellationToken cancellationToken, string id, UpdateServiceRequest request) =>
        {
            request.Id = id;
            
            var result = await mediator.Send(request, cancellationToken);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
        });
    }
}