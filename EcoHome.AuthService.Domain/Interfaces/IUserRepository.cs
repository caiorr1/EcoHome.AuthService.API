using EcoHome.AuthService.Domain.Entities;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByEmailAsync(string email);
        Task AddAsync(UserEntity user);
        Task<UserEntity> GetByIdAsync(int id);
        Task UpdateAsync(UserEntity user);
        Task DeleteAsync(UserEntity user);
    }
}
