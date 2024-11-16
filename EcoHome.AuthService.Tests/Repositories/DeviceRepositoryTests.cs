using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace EcoHome.AuthService.Tests.Repositories
{
    public class DeviceRepositoryTests
    {
        private AuthDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new AuthDbContext(options);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Device()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new DeviceRepository(dbContext);

            var device = new DeviceEntity
            {
                Name = "Smart Light",
                PowerConsumption = 15.5m,
                Location = "Living Room",
                UserId = 1
            };

            await dbContext.Devices.AddAsync(device);
            await dbContext.SaveChangesAsync();

            var retrievedDevice = await repository.GetByIdAsync(device.Id);

            Assert.NotNull(retrievedDevice);
            Assert.Equal("Smart Light", retrievedDevice.Name);
            Assert.Equal(15.5m, retrievedDevice.PowerConsumption);
        }
    }
}
