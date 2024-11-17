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

        /// <summary>
        /// Atualiza um alerta existente.
        /// </summary>
        /// <param name="id">ID do alerta.</param>
        /// <param name="dto">Dados atualizados do alerta.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlert(int id, [FromBody] AlertCreateDto dto)
        {
            if (id <= 0 || dto == null || !ModelState.IsValid)
                return BadRequest("Dados inválidos");

            var result = await _alertService.UpdateAlertAsync(id, dto);
            if (!result)
                return NotFound($"Alerta com ID {id} não encontrado");

            return Ok("Alerta atualizado com sucesso");
        }

        /// <summary>
        /// Exclui um alerta.
        /// </summary>
        /// <param name="id">ID do alerta.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var result = await _alertService.DeleteAlertAsync(id);
            if (!result)
                return NotFound($"Alerta com ID {id} não encontrado");

            return Ok("Alerta excluído com sucesso");
        }
    }
}