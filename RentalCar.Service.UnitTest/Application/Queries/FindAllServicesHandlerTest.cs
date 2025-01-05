using FluentAssertions;
using Moq;
using RentalCar.Service.Application.Handlers;
using RentalCar.Service.Application.Queries.Request;
using RentalCar.Service.Core.Entities;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;

namespace RentalCar.Service.UnitTest.Application.Queries;

public class FindAllServicesHandlerTest
{
    [Fact]
    public async void FindAllServices_Executed_Return_FindServiceResponse()
    {
        // Arrange
        var repositoryMock = new Mock<IServiceRepository>();
        var loggerServiceMock = new Mock<ILoggerService>();
        var prometheusServiceMock = new Mock<IPrometheusService>();

        var services = new List<Services>
        {
            new Services
            {
                Id = "123456789",
                Name = "Teste",
                Description = "Teste"
            }, 
            new Services
            {
                Id = "123456789",
                Name = "Teste",
                Description = "Teste"
            },
            new Services
            {
                Id = "123456789",
                Name = "Teste",
                Description = "Teste"
            }
        };
        
        repositoryMock.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(services);
        
        var findAllServicesHandler = new FindAllServicesHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);
        
        // Act
        var result = await findAllServicesHandler.Handle(new FindAllServicesRequest(1, 10), CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Datas.Should().NotBeNull();
        result.Message.Should().NotBeNullOrEmpty();
        result.Succeeded.Should().BeTrue();
        result.Datas.Count.Should().Be(services.Count);
        
        repositoryMock.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once());
    }
}