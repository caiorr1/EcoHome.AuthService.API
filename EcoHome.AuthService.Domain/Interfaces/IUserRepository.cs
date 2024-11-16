using EcoHome.AuthService.Domain.Entities;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByEmailAsync(string email);
        Task AddAsync(UserEntity user);
    }
}
