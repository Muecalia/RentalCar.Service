namespace RentalCar.Service.Application.Queries.Response;

public record FindServiceResponse(string Id, string Name, string Description, string CreateAt, string? UpdateAt);