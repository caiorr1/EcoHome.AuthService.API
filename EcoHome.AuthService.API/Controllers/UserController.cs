﻿using EcoHome.AuthService.Application.Services;
using EcoHome.AuthService.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcoHome.AuthService.API.Controllers
{
    /// <summary>
    /// Controller para gerenciar usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// Construtor da UserController.
        /// </summary>
        /// <param name="userService">O serviço de usuários.</param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="dto">Dados do usuário a ser criado.</param>
        /// <returns>O usuário criado.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUserByEmail), new { email = result.Email }, result);
        }

        /// <summary>
        /// Obtém um usuário pelo e-mail.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <returns>Os dados do usuário.</returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}