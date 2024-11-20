using Application.DTOs;

namespace Application.Services
{
    public class ApiResponseService
    {
        // Método para manejar respuestas exitosas
        public ApiResponse<T> Success<T>(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>(true, message, data);
        }

        // Método para manejar respuestas de error controladas
        public ApiResponse<T> Error<T>(string message, T? data = default)
        {
            return new ApiResponse<T>(false, message, data);
        }

        // Método para manejar errores inesperados o excepciones
        public ApiResponse<T> Exception<T>(string message)
        {
            return new ApiResponse<T>(false, message, default);
        }
    }
}
