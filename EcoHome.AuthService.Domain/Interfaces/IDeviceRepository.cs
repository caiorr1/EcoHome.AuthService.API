using EcoHome.AuthService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Interfaces
{
    public interface IDeviceRepository
    {
        Task<DeviceEntity> GetByIdAsync(int id);
        Task<IEnumerable<DeviceEntity>> GetByUserIdAsync(int userId);
        Task AddAsync(DeviceEntity device);
        Task UpdateAsync(DeviceEntity device);
        Task DeleteAsync(DeviceEntity device);
    }
}