using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EcoHome.AuthService.Tests.Integration
{
    public class FullFlowIntegrationTests
    {
        private AuthDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new AuthDbContext(options);
        }

        [Fact]
        public async Task FullFlow_CreateUser_And_AssociateDevice()
        {
            var dbContext = GetInMemoryDbContext();

            var userRepo = new UserRepository(dbContext);
            var deviceRepo = new DeviceRepository(dbContext);

            dbContext.Users.RemoveRange(dbContext.Users);
            dbContext.Devices.RemoveRange(dbContext.Devices);
            await dbContext.SaveChangesAsync();

            var user = new UserEntity
            {
                Name = "Test User",
                Email = "testuser@example.com",
                PasswordHash = "hashedpassword"
            };

            var device = new DeviceEntity
            {
                Name = "Smart Light",
                PowerConsumption = 12.5m,
                Location = "Bedroom"
            };

            await userRepo.AddAsync(user);
            device.UserId = user.Id;
            await deviceRepo.AddAsync(device);

            var retrievedUser = await dbContext.Users.Include(u => u.Devices).FirstOrDefaultAsync(u => u.Email == "testuser@example.com");

            Assert.NotNull(retrievedUser);
            Assert.Single(retrievedUser.Devices);
            Assert.Equal("Smart Light", retrievedUser.Devices.First().Name);
        }
    }
}
