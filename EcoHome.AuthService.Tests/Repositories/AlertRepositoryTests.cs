using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Entities.Common;
using EcoHome.AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace EcoHome.AuthService.Tests.Repositories
{
    public class AlertRepositoryTests
    {
        private AuthDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new AuthDbContext(options);
        }

        [Fact]
        public async Task UpdateStatusAsync_Should_Update_Alert_Status()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new AlertRepository(dbContext);

            var alert = new AlertEntity
            {
                Message = "Test Alert",
                UserId = 1,
                Status = StatusEnum.Active
            };

            await dbContext.Alerts.AddAsync(alert);
            await dbContext.SaveChangesAsync();

            await repository.UpdateStatusAsync(alert.Id, false);
            var updatedAlert = await dbContext.Alerts.FindAsync(alert.Id);

            Assert.NotNull(updatedAlert);
            Assert.False(updatedAlert.Status == StatusEnum.Active);
        }

        [Fact]
        public async Task AddAsync_Should_Save_Alert()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new AlertRepository(dbContext);

            var alert = new AlertEntity
            {
                Message = "Energy usage exceeded!",
                UserId = 2,
                Status = StatusEnum.Active
            };

            await repository.AddAsync(alert);
            var savedAlert = await dbContext.Alerts.FindAsync(alert.Id);

            Assert.NotNull(savedAlert);
            Assert.Equal("Energy usage exceeded!", savedAlert.Message);
        }
    }
}
