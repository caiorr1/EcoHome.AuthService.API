using EcoHome.AuthService.Domain.Dtos;
using EcoHome.AuthService.Domain.Entities;
using EcoHome.AuthService.Domain.Interfaces;

namespace EcoHome.AuthService.Application.Services
{
    /// <summary>
    /// Serviço responsável pelo gerenciamento de usuários.
    /// </summary>
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Construtor do serviço de usuário.
        /// </summary>
        /// <param name="userRepository">O repositório de usuários.</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="dto">Os dados do usuário a ser criado.</param>
        /// <returns>Os dados do usuário criado.</returns>
        /// <exception cref="ArgumentException">Lançado quando os dados são inválidos.</exception>
        public async Task<UserResponseDto> CreateUserAsync(UserCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("User name cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email cannot be empty.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password cannot be empty.");

            var userEntity = new UserEntity
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _userRepository.AddAsync(userEntity);

            return new UserResponseDto
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email
            };
        }

        /// <summary>
        /// Obtém um usuário pelo e-mail.
        /// </summary>
        /// <param name="email">O e-mail do usuário.</param>
        /// <returns>Os dados do usuário correspondente ou <c>null</c> se não encontrado.</returns>
        public async Task<UserResponseDto> GetUserByEmailAsync(string email)
        {
            var userEntity = await _userRepository.GetByEmailAsync(email);
            if (userEntity == null) return null;

            return new UserResponseDto
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email
            };
        }
    }
}
