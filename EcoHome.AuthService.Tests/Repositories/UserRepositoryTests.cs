using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace EcoHome.AuthService.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private AuthDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new AuthDbContext(options);
        }

        [Fact]
        public async Task GetByEmailAsync_Should_Return_Correct_User()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new UserRepository(dbContext);

            var user = new UserEntity
            {
                Name = "Test User",
                Email = "test@example.com",
                PasswordHash = "hashedpassword"
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var retrievedUser = await repository.GetByEmailAsync("test@example.com");

            Assert.NotNull(retrievedUser);
            Assert.Equal("Test User", retrievedUser.Name);
            Assert.Equal("test@example.com", retrievedUser.Email);
        }

        [Fact]
        public async Task AddAsync_Should_Save_User_To_Database()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new UserRepository(dbContext);

            var user = new UserEntity
            {
                Name = "New User",
                Email = "newuser@example.com",
                PasswordHash = "securepassword"
            };

            await repository.AddAsync(user);
            var savedUser = await dbContext.Users.FindAsync(user.Id);

            Assert.NotNull(savedUser);
            Assert.Equal("New User", savedUser.Name);
        }
    }
}
