using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ConsumptionLogEntity> GetByIdAsync(int id)
        {
            return await _context.Set<ConsumptionLogEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(ConsumptionLogEntity log)
        {
            _context.Set<ConsumptionLogEntity>().Update(log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ConsumptionLogEntity log)
        {
            _context.Set<ConsumptionLogEntity>().Remove(log);
            await _context.SaveChangesAsync();
        }
    }
}