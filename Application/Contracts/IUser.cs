using Application.DTOs;

namespace Application.Contracts
{
    public interface IUser
    {
        // Modificamos el tipo de retorno a ApiResponse<T>
        Task<ApiResponse<RegistrationResponse>> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<ApiResponse<LoginResponse>> LoginUserAsync(LoginDTO loginDTO);
    }
}
