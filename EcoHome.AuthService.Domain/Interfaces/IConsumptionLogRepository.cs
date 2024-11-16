using EcoHome.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Interfaces
{
    public interface IConsumptionLogRepository
    {
        Task<IEnumerable<ConsumptionLogEntity>> GetByDeviceIdAsync(int deviceId);
        Task AddAsync(ConsumptionLogEntity log);
    }
}
