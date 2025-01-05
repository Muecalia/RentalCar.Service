using FluentAssertions;
using Moq;
using RentalCar.Service.Application.Commands.Request;
using RentalCar.Service.Application.Handlers;
using RentalCar.Service.Core.Entities;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;

namespace RentalCar.Service.UnitTest.Application.Commands;

public class UpdadeServiceHandlerTest
{
    [Fact]
    public async void UpdadeService_Executed_Return_InputServiceResponse()
    {
        // Arrange
        var repositoryMock = new Mock<IServiceRepository>();
        var loggerServiceMock = new Mock<ILoggerService>();
        var prometheusServiceMock = new Mock<IPrometheusService>();

        var request = new UpdateServiceRequest
        {
            Id = "123456789",
            Name = "Teste",
            Description = "Teste"
        };
        
        var services = new Services
        {
            Id = "123456789",
            Name = "Teste",
            Description = "Teste"
        };
        
        repositoryMock.Setup(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(services);
        repositoryMock.Setup(repo => repo.Update(It.IsAny<Services>(), It.IsAny<CancellationToken>()));
        
        var updateServiceHandler = new UpdadeServiceHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);
        
        // Act
        var result = await updateServiceHandler.Handle(request, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Message.Should().NotBeNullOrEmpty();
        result.Succeeded.Should().BeTrue();
        result.Data.Date.Should().Be(services.UpdatedAt?.ToShortDateString());
        
        repositoryMock.Verify(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(repo => repo.Update(It.IsAny<Services>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}