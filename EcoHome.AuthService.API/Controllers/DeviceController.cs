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
    }
}
