using FluentAssertions;
using RentalCar.Service.Core.Entities;

namespace RentalCar.Service.UnitTest.Core;

public class ServiceTest
{
    [Fact]
    public void CreateServiceSuccess()
    {
        // Arrange
        var newService = new Services
        {
            Id = "123456789",
            Name = "TestService",
            Description = "Test Description"
        };

        // Act


        // Assert   
        newService.Should().NotBeNull();
        newService.CreatedAt.Date.Should().Be(DateTime.UtcNow.Date);
        newService.Name.Should().NotBeNullOrEmpty();

    }
}