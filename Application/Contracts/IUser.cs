using Application.DTOs;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IUser
    {
        // Modificamos el tipo de retorno a ApiResponse<T>
        Task<ApiResponse<RegistrationResponse<ApplicationUser>>> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<ApiResponse<LoginResponse>> LoginUserAsync(LoginDTO loginDTO);
    }
}
