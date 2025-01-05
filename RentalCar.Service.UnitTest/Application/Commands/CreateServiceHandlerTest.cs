using FluentAssertions;
using Moq;
using RentalCar.Service.Application.Commands.Request;
using RentalCar.Service.Application.Handlers;
using RentalCar.Service.Core.Entities;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;

namespace RentalCar.Service.UnitTest.Application.Commands;

public class CreateServiceHandlerTest
{
    [Fact]
    public async void CreateService_Executed_Return_InputServiceResponse()
    {
        // Arrange
        var repositoryMock = new Mock<IServiceRepository>();
        var loggerServiceMock = new Mock<ILoggerService>();
        var prometheusServiceMock = new Mock<IPrometheusService>();
        
        var request = new CreateServiceRequest
        {
            Name = "Teste",
            Description = "Teste"
        };

        var services = new Services
        {
            Id = "123456789",
            Name = "Teste",
            Description = "Teste"
        };

        repositoryMock.Setup(repo => repo.IsServiceExist(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        repositoryMock.Setup(repo => repo.Create(It.IsAny<Services>(), It.IsAny<CancellationToken>())).ReturnsAsync(services);
        
        var createServiceHandler = new CreateServiceHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

        // Act
        var result = await createServiceHandler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Name.Should().NotBeNullOrEmpty();
        result.Data.Date.Should().Be(services.CreatedAt.ToShortDateString());
        
        repositoryMock.Verify(repo => repo.IsServiceExist(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(repo => repo.Create(It.IsAny<Services>(), It.IsAny<CancellationToken>()), Times.Once);

    }
}