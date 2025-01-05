using FluentAssertions;
using Moq;
using RentalCar.Service.Application.Commands.Request;
using RentalCar.Service.Application.Handlers;
using RentalCar.Service.Core.Entities;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;

namespace RentalCar.Service.UnitTest.Application.Commands;

public class DeleteServiceHandlerTest
{
    [Fact]
    public async void DeleteService_Executed_Return_InputServiceResponse()
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

        var request = new DeleteServiceRequest("123456789");

        repositoryMock.Setup(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Services());
        repositoryMock.Setup(repo => repo.Delete(It.IsAny<Services>(), It.IsAny<CancellationToken>()));
        
        var deleteServiceHandler = new DeleteServiceHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);
        
        // Act
        var result = await deleteServiceHandler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Message.Should().NotBeNullOrEmpty();
        result.Succeeded.Should().BeTrue();
        result.Data.Date.Should().Be(services.DeletedAt?.ToShortDateString());
        
        repositoryMock.Verify(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(repo => repo.Delete(It.IsAny<Services>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
}