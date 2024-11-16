using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Entities.Common;
using EcoHome.AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Infrastructure.Data
{
    public class AlertRepository : BaseRepository<AlertEntity>, IAlertRepository
    {
        public AlertRepository(AuthDbContext context) : base(context) { }

        public async Task<IEnumerable<AlertEntity>> GetByUserIdAsync(int userId)
        {
            return await _context.Set<AlertEntity>()
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(int alertId, bool isActive)
        {
            var alert = await _context.Set<AlertEntity>().FindAsync(alertId);
            if (alert != null)
            {
                alert.Status = isActive ? StatusEnum.Active : StatusEnum.Inactive;
                _context.Update(alert);
                await _context.SaveChangesAsync();
            }
        }
    }
}
