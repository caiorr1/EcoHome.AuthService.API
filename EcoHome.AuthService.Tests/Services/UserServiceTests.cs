using EcoHome.AuthService.Application.Services;
using EcoHome.AuthService.Domain.Dtos;
using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUser_Should_Return_UserResponseDto()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);

            var createDto = new UserCreateDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "password123"
            };

            mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<UserEntity>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await userService.CreateUserAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test User", result.Name);
            Assert.Equal("test@example.com", result.Email);
            mockUserRepository.Verify(repo => repo.AddAsync(It.IsAny<UserEntity>()), Times.Once);
        }
    }
}
