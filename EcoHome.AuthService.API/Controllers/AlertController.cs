using EcoHome.AuthService.Application.Services;
using EcoHome.AuthService.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcoHome.AuthService.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar alertas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly AlertService _alertService;

        /// <summary>
        /// Construtor da AlertController.
        /// </summary>
        /// <param name="alertService">O serviço de alertas.</param>
        public AlertController(AlertService alertService)
        {
            _alertService = alertService;
        }

        /// <summary>
        /// Cria um novo alerta.
        /// </summary>
        /// <param name="dto">Dados do alerta a ser criado.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAlert([FromBody] AlertCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _alertService.CreateAlertAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Obtém os alertas de um usuário.
        /// </summary>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Lista de alertas.</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAlertsByUserId(int userId)
        {
            var result = await _alertService.GetAlertsByUserIdAsync(userId);
            return Ok(result);
        }
    }
}
