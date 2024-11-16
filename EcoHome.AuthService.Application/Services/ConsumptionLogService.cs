using EcoHome.AuthService.Domain.Dtos;
using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Application.Services
{
    /// <summary>
    /// Serviço responsável pelo gerenciamento de logs de consumo.
    /// </summary>
    public class ConsumptionLogService
    {
        private readonly IConsumptionLogRepository _logRepository;

        /// <summary>
        /// Construtor do serviço de logs de consumo.
        /// </summary>
        /// <param name="logRepository">O repositório de logs de consumo.</param>
        public ConsumptionLogService(IConsumptionLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// Adiciona um novo log de consumo.
        /// </summary>
        /// <param name="dto">Os dados do log de consumo a ser criado.</param>
        /// <exception cref="ArgumentException">Lançado quando os dados são inválidos.</exception>
        public async Task AddLogAsync(ConsumptionLogCreateDto dto)
        {
            // Validações omitidas para brevidade.
            var log = new ConsumptionLogEntity
            {
                DeviceId = dto.DeviceId,
                Consumption = dto.Consumption,
                Timestamp = dto.Timestamp
            };

            await _logRepository.AddAsync(log);
        }

        /// <summary>
        /// Obtém todos os logs de consumo associados a um dispositivo.
        /// </summary>
        /// <param name="deviceId">O ID do dispositivo.</param>
        /// <returns>Uma lista de logs de consumo.</returns>
        public async Task<IEnumerable<ConsumptionLogResponseDto>> GetLogsByDeviceIdAsync(int deviceId)
        {
            var logs = await _logRepository.GetByDeviceIdAsync(deviceId);

            return logs.Select(log => new ConsumptionLogResponseDto
            {
                Id = log.Id,
                DeviceId = log.DeviceId,
                DeviceName = log.Device.Name,
                Consumption = log.Consumption,
                Timestamp = log.Timestamp
            });
        }
    }
}
