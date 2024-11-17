using EcoHome.AuthService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Interfaces
{
    public interface IConsumptionLogRepository
    {
        Task<IEnumerable<ConsumptionLogEntity>> GetByDeviceIdAsync(int deviceId);
        Task AddAsync(ConsumptionLogEntity log);
        Task<ConsumptionLogEntity> GetByIdAsync(int id);
        Task UpdateAsync(ConsumptionLogEntity log);
        Task DeleteAsync(ConsumptionLogEntity log);
    }
}