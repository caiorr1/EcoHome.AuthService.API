using EcoHome.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Interfaces
{
    public interface IAlertRepository
    {
        Task<IEnumerable<AlertEntity>> GetByUserIdAsync(int userId);
        Task AddAsync(AlertEntity alert);
        Task UpdateStatusAsync(int alertId, bool isActive);
    }
}
