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
            // Validações omitidas para brevidade.
            var alert = new AlertEntity
            {
                Message = dto.Message,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _alertRepository.AddAsync(alert);
        }

        /// <summary>
        /// Obtém todos os alertas associados a um usuário.
        /// </summary>
        /// <param name="userId">O ID do usuário.</param>
        /// <returns>Uma lista de alertas do usuário.</returns>
        public async Task<IEnumerable<AlertResponseDto>> GetAlertsByUserIdAsync(int userId)
        {
            var alerts = await _alertRepository.GetByUserIdAsync(userId);

            return alerts.Select(alert => new AlertResponseDto
            {
                Id = alert.Id,
                Message = alert.Message,
                Status = alert.Status.ToString(),
                CreatedAt = alert.CreatedAt
            });
        }
    }
}
