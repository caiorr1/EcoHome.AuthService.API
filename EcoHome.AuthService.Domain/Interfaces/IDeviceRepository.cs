using EcoHome.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Interfaces
{
    public interface IDeviceRepository
    {
        Task<DeviceEntity> GetByIdAsync(int id);
        Task<IEnumerable<DeviceEntity>> GetByUserIdAsync(int userId);
        Task AddAsync(DeviceEntity device);
        Task UpdateAsync(DeviceEntity device);
        Task DeleteAsync(int id);
    }
}
