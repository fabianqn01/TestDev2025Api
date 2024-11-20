using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services; // Asegúrate de tener la referencia a los servicios

namespace Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ApiResponseService _apiResponseService;  // Inyectamos el servicio de respuestas

        public UserRepository(AppDbContext context, IConfiguration configuration, ApiResponseService apiResponseService)
        {
            _context = context;
            _configuration = configuration;
            _apiResponseService = apiResponseService;  // Inicializamos el servicio de respuestas
        }

        public async Task<ApiResponse<LoginResponse>> LoginUserAsync(LoginDTO loginDTO)
        {
            try
            {
                var getUser = await _context.Users
                    .Include(u => u.Roles) // Incluir roles relacionados
                    .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

                if (getUser == null)
                    return _apiResponseService.Error<LoginResponse>("User not found");

                bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
                if (!checkPassword)
                    return _apiResponseService.Error<LoginResponse>("Invalid credentials");

                // Crear DTO de usuario con roles
                var userDto = new UserDto(
                    getUser.Id,
                    getUser.Name!,
                    getUser.Email!,
                    getUser.Roles.Select(r => r.Name).ToList()
                );

                // Generar token
                var token = GenerateJWToken(getUser);

                // Devolvemos la respuesta con éxito
                return _apiResponseService.Success(new LoginResponse(userDto, token), "Login successful");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolvemos una respuesta de excepción
                return _apiResponseService.Exception<LoginResponse>("An error occurred during login");
            }
        }

        public async Task<ApiResponse<RegistrationResponse>> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var getUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerUserDTO.Email!);
                if (getUser != null)
                    return _apiResponseService.Error<RegistrationResponse>("User already exists");

                _context.Users.Add(new ApplicationUser()
                {
                    Name = registerUserDTO.Name,
                    Email = registerUserDTO.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password)
                });

                await _context.SaveChangesAsync();

                return _apiResponseService.Success(new RegistrationResponse(true, "Registration completed"), "User registered successfully");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolvemos una respuesta de excepción
                return _apiResponseService.Exception<RegistrationResponse>("An error occurred during registration");
            }
        }

        private string GenerateJWToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            // Agregar los roles como claims
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
