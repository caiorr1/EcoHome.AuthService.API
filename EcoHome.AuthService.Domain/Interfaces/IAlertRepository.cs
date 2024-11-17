using EcoHome.AuthService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Interfaces
{
    public interface IAlertRepository
    {
        Task<IEnumerable<AlertEntity>> GetByUserIdAsync(int userId);
        Task AddAsync(AlertEntity alert);
        Task<AlertEntity> GetByIdAsync(int id);
        Task UpdateAsync(AlertEntity alert);
        Task DeleteAsync(AlertEntity alert);
    }
}
