using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EcoHome.AuthService.Tests.Repositories
{
    public class ConsumptionLogRepositoryTests
    {
        private AuthDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new AuthDbContext(options);
        }

        [Fact]
        public async Task AddAsync_Should_Add_ConsumptionLog_To_Database()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new ConsumptionLogRepository(dbContext);

            var log = new ConsumptionLogEntity
            {
                Consumption = 120.5m,
                DeviceId = 1,
                Timestamp = DateTime.UtcNow
            };

            await repository.AddAsync(log);
            var retrievedLog = await dbContext.ConsumptionLogs.FirstOrDefaultAsync();

            Assert.NotNull(retrievedLog);
            Assert.Equal(120.5m, retrievedLog.Consumption);
            Assert.Equal(1, retrievedLog.DeviceId);
        }

        [Fact]
        public async Task GetByDeviceId_Should_Return_Correct_Logs()
        {
            var dbContext = GetInMemoryDbContext();

            dbContext.ConsumptionLogs.RemoveRange(dbContext.ConsumptionLogs);
            await dbContext.SaveChangesAsync();

            var repository = new ConsumptionLogRepository(dbContext);

            var log1 = new ConsumptionLogEntity { DeviceId = 1, Consumption = 50.0m, Timestamp = DateTime.UtcNow };
            var log2 = new ConsumptionLogEntity { DeviceId = 1, Consumption = 70.0m, Timestamp = DateTime.UtcNow };

            await dbContext.ConsumptionLogs.AddRangeAsync(log1, log2);
            await dbContext.SaveChangesAsync();

            var logs = (await repository.GetByDeviceIdAsync(1)).ToList();

            Assert.NotEmpty(logs);
            Assert.Equal(2, logs.Count);
        }
    }
}
