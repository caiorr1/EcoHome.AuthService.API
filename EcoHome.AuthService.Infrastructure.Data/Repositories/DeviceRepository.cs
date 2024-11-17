using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Infrastructure.Data
{
    public class DeviceRepository : BaseRepository<DeviceEntity>, IDeviceRepository
    {
        public DeviceRepository(AuthDbContext context) : base(context) { }

        public async Task<DeviceEntity> GetByIdAsync(int id)
        {
            return await _context.Set<DeviceEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<DeviceEntity>> GetByUserIdAsync(int userId)
        {
            return await _context.Set<DeviceEntity>()
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(DeviceEntity device)
        {
            _context.Set<DeviceEntity>().Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DeviceEntity device)
        {
            _context.Set<DeviceEntity>().Remove(device);
            await _context.SaveChangesAsync();
        }
    }
}
