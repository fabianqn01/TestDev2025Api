namespace Application.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } // Indica si la operación fue exitosa
        public string Message { get; set; } = string.Empty; // Mensaje descriptivo
        public T? Data { get; set; } // Datos relevantes (puede ser null en caso de error)

        public ApiResponse() { }

        public ApiResponse(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
