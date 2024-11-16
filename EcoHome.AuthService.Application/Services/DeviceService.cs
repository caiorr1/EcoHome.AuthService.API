using EcoHome.AuthService.Domain.Dtos;
using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;

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
            // Validações omitidas para brevidade.
            var device = new DeviceEntity
            {
                Name = dto.Name,
                PowerConsumption = dto.PowerConsumption,
                Location = dto.Location,
                UserId = dto.UserId
            };

            await _deviceRepository.AddAsync(device);

            return new DeviceResponseDto
            {
                Id = device.Id,
                Name = device.Name,
                PowerConsumption = device.PowerConsumption,
                Location = device.Location,
                Status = device.Status.ToString()
            };
        }

        /// <summary>
        /// Obtém todos os dispositivos associados a um usuário.
        /// </summary>
        /// <param name="userId">O ID do usuário.</param>
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
                Status = device.Status.ToString()
            });
        }
    }
}
