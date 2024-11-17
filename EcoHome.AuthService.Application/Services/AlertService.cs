using EcoHome.AuthService.Domain.Dtos;
using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;

namespace EcoHome.AuthService.Application.Services
{
    /// <summary>
    /// Serviço responsável pelo gerenciamento de alertas.
    /// </summary>
    public class AlertService
    {
        private readonly IAlertRepository _alertRepository;

        /// <summary>
        /// Construtor do serviço de alertas.
        /// </summary>
        /// <param name="alertRepository">O repositório de alertas.</param>
        public AlertService(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        /// <summary>
        /// Cria um novo alerta.
        /// </summary>
        /// <param name="dto">Os dados do alerta a ser criado.</param>
        /// <exception cref="ArgumentException">Lançado quando os dados são inválidos.</exception>
        public async Task CreateAlertAsync(AlertCreateDto dto)
        {
            var alert = new AlertEntity
            {
                Message = dto.Message,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _alertRepository.AddAsync(alert);
        }

        /// <summary>
        /// Atualiza um alerta existente.
        /// </summary>
        /// <param name="id">ID do alerta.</param>
        /// <param name="dto">Dados atualizados do alerta.</param>
        /// <returns>Verdadeiro se a atualização for bem-sucedida; caso contrário, falso.</returns>
        public async Task<bool> UpdateAlertAsync(int id, AlertCreateDto dto)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null) return false;

            alert.Message = dto.Message;
            alert.Status = dto.Status;
            alert.UpdatedAt = DateTime.UtcNow;

            await _alertRepository.UpdateAsync(alert);
            return true;
        }

        /// <summary>
        /// Obtém todos os alertas associados a um usuário.
        /// </summary>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Uma lista de alertas do usuário.</returns>
        public async Task<IEnumerable<AlertResponseDto>> GetAlertsByUserIdAsync(int userId)
        {
            var alerts = await _alertRepository.GetByUserIdAsync(userId);
            return alerts.Select(a => new AlertResponseDto
            {
                Id = a.Id,
                Message = a.Message,
                Status = a.Status.ToString(),
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            });
        }

        /// <summary>
        /// Exclui um alerta.
        /// </summary>
        /// <param name="id">ID do alerta.</param>
        /// <returns>Verdadeiro se a exclusão for bem-sucedida; caso contrário, falso.</returns>
        public async Task<bool> DeleteAlertAsync(int id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null) return false;

            await _alertRepository.DeleteAsync(alert);
            return true;
        }
    }
}
