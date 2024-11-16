using EcoHome.AuthService.Application.Services;
using EcoHome.AuthService.Domain.Dtos;
using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Tests.Services
{
    public class DeviceServiceTests
    {
        [Fact]
        public async Task GetDevicesByUserId_Should_Return_Devices_For_User()
        {
            var mockRepo = new Mock<IDeviceRepository>();
            var service = new DeviceService(mockRepo.Object);

            var devices = new List<DeviceEntity>
            {
                new DeviceEntity { Id = 1, Name = "Smart Light", UserId = 1 },
                new DeviceEntity { Id = 2, Name = "Smart Thermostat", UserId = 1 }
            };

            mockRepo.Setup(repo => repo.GetByUserIdAsync(1)).ReturnsAsync(devices);

            var result = await service.GetDevicesByUserIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, d => d.Name == "Smart Light");
        }
    }
}
