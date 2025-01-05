using FluentAssertions;
using Moq;
using RentalCar.Service.Application.Handlers;
using RentalCar.Service.Application.Queries.Request;
using RentalCar.Service.Core.Entities;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;

namespace RentalCar.Service.UnitTest.Application.Queries;

public class FindServiceByIdHandlerTest
{
    [Fact]
    public async void FindServiceById_Executed_Return_FindServiceResponse()
    {
        // Arrange
        var repositoryMock = new Mock<IServiceRepository>();
        var loggerServiceMock = new Mock<ILoggerService>();
        var prometheusServiceMock = new Mock<IPrometheusService>();
        
        var services = new Services
        {
            Id = "123456789",
            Name = "Teste",
            Description = "Teste"
        };
        
        repositoryMock.Setup(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(services);
        
        var findServiceByIdHandler = new FindServiceByIdHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);
        
        // Act
        var result = await findServiceByIdHandler.Handle(new FindServiceByIdRequest("123456789"), CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Message.Should().NotBeNullOrEmpty();
        result.Succeeded.Should().BeTrue();
        
        repositoryMock.Verify(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());

    }
}