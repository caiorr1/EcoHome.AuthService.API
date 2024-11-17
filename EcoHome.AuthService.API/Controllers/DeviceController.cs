using EcoHome.AuthService.Application.Services;
using EcoHome.AuthService.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcoHome.AuthService.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar dispositivos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceService _deviceService;

        /// <summary>
        /// Construtor da DeviceController.
        /// </summary>
        /// <param name="deviceService">O serviço de dispositivos.</param>
        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Adiciona um novo dispositivo.
        /// </summary>
        /// <param name="dto">Dados do dispositivo a ser adicionado.</param>
        /// <returns>O dispositivo criado.</returns>
        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] DeviceCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _deviceService.AddDeviceAsync(dto);
            return CreatedAtAction(nameof(GetDevicesByUserId), new { userId = dto.UserId }, result);
        }

        /// <summary>
        /// Obtém todos os dispositivos de um usuário.
        /// </summary>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Lista de dispositivos.</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDevicesByUserId(int userId)
        {
            var result = await _deviceService.GetDevicesByUserIdAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Atualiza um dispositivo existente.
        /// </summary>
        /// <param name="id">ID do dispositivo.</param>
        /// <param name="dto">Dados atualizados do dispositivo.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] DeviceCreateDto dto)
        {
            if (id <= 0 || dto == null || !ModelState.IsValid)
                return BadRequest("Dados inválidos");

            var result = await _deviceService.UpdateDeviceAsync(id, dto);
            if (!result)
                return NotFound($"Dispositivo com ID {id} não encontrado");

            return Ok("Dispositivo atualizado com sucesso");
        }

        /// <summary>
        /// Exclui um dispositivo.
        /// </summary>
        /// <param name="id">ID do dispositivo.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var result = await _deviceService.DeleteDeviceAsync(id);
            if (!result)
                return NotFound($"Dispositivo com ID {id} não encontrado");

            return Ok("Dispositivo excluído com sucesso");
        }
    }
}
