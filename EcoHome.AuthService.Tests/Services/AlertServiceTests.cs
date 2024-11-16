using EcoHome.AuthService.Application.Services;
using EcoHome.AuthService.Domain.Dtos;
using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Tests.Services
{
    public class AlertServiceTests
    {
        [Fact]
        public async Task CreateAlert_Should_Call_Repository_With_Correct_Data()
        {
            // Arrange
            var mockAlertRepository = new Mock<IAlertRepository>();
            var alertService = new AlertService(mockAlertRepository.Object);

            var alertDto = new AlertCreateDto
            {
                Message = "High energy usage detected!",
                UserId = 1
            };

            mockAlertRepository.Setup(repo => repo.AddAsync(It.IsAny<AlertEntity>()))
                .Returns(Task.CompletedTask);

            // Act
            await alertService.CreateAlertAsync(alertDto);

            // Assert
            mockAlertRepository.Verify(repo => repo.AddAsync(It.Is<AlertEntity>(
                alert => alert.Message == "High energy usage detected!" && alert.UserId == 1
            )), Times.Once);
        }
    }
}
