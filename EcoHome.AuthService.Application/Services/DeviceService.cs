using EcoHome.AuthService.Domain.Dtos;
using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EcoHome.AuthService.Application.Services
{
    /// <summary>
    /// Serviço responsável pelo gerenciamento de dispositivos.
    /// </summary>
    public class DeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        /// <summary>
        /// Construtor do serviço de dispositivos.
        /// </summary>
        /// <param name="deviceRepository">O repositório de dispositivos.</param>
        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        /// <summary>
        /// Adiciona um novo dispositivo.
        /// </summary>
        /// <param name="dto">Os dados do dispositivo a ser criado.</param>
        /// <returns>Os dados do dispositivo criado.</returns>
        /// <exception cref="ArgumentException">Lançado quando os dados são inválidos.</exception>
        public async Task<DeviceResponseDto> AddDeviceAsync(DeviceCreateDto dto)
        {
            var device = new DeviceEntity
            {
                Name = dto.Name,
                PowerConsumption = dto.PowerConsumption,
                Location = dto.Location,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _deviceRepository.AddAsync(device);

            return new DeviceResponseDto
            {
                Id = device.Id,
                Name = device.Name,
                PowerConsumption = device.PowerConsumption,
                Location = device.Location,
                Status = device.Status.ToString(),
                CreatedAt = device.CreatedAt
            };
        }

        /// <summary>
        /// Atualiza um dispositivo existente.
        /// </summary>
        /// <param name="id">ID do dispositivo.</param>
        /// <param name="dto">Dados atualizados do dispositivo.</param>
        /// <returns>Verdadeiro se a atualização for bem-sucedida; caso contrário, falso.</returns>
        public async Task<bool> UpdateDeviceAsync(int id, DeviceCreateDto dto)
        {
            var device = await _deviceRepository.GetByIdAsync(id);
            if (device == null) return false;

            device.Name = dto.Name;
            device.PowerConsumption = dto.PowerConsumption;
            device.Location = dto.Location;
            device.UpdatedAt = DateTime.UtcNow;

            await _deviceRepository.UpdateAsync(device);
            return true;
        }

        /// <summary>
        /// Obtém todos os dispositivos associados a um usuário.
        /// </summary>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Uma lista de dispositivos do usuário.</returns>
        public async Task<IEnumerable<DeviceResponseDto>> GetDevicesByUserIdAsync(int userId)
        {
            var devices = await _deviceRepository.GetByUserIdAsync(userId);
            return devices.Select(device => new DeviceResponseDto
            {
                Id = device.Id,
                Name = device.Name,
                PowerConsumption = device.PowerConsumption,
                Location = device.Location,
                Status = device.Status.ToString(),
                CreatedAt = device.CreatedAt,
                UpdatedAt = device.UpdatedAt
            });
        }

        /// <summary>
        /// Exclui um dispositivo.
        /// </summary>
        /// <param name="id">ID do dispositivo.</param>
        /// <returns>Verdadeiro se a exclusão for bem-sucedida; caso contrário, falso.</returns>
        public async Task<bool> DeleteDeviceAsync(int id)
        {
            var device = await _deviceRepository.GetByIdAsync(id);
            if (device == null) return false;

            await _deviceRepository.DeleteAsync(device);
            return true;
        }
    }
}
