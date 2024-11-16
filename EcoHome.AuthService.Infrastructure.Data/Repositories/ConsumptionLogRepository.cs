using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Infrastructure.Data
{
    public class ConsumptionLogRepository : BaseRepository<ConsumptionLogEntity>, IConsumptionLogRepository
    {
        public ConsumptionLogRepository(AuthDbContext context) : base(context) { }

        public async Task<IEnumerable<ConsumptionLogEntity>> GetByDeviceIdAsync(int deviceId)
        {
            return await _context.Set<ConsumptionLogEntity>()
                .Where(cl => cl.DeviceId == deviceId)
                .ToListAsync();
        }
    }
}
