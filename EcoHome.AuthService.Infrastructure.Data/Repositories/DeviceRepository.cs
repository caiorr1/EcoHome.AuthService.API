using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoHome.AuthService.Infrastructure.Data
{
    public class DeviceRepository : BaseRepository<DeviceEntity>, IDeviceRepository
    {
        public DeviceRepository(AuthDbContext context) : base(context) { }

        public async Task<IEnumerable<DeviceEntity>> GetByUserIdAsync(int userId)
        {
            return await _context.Set<DeviceEntity>()
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }
    }
}
