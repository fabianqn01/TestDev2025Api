namespace Application.DTOs
{
    public record RegistrationResponse<T>(bool Flag, string Message, T? Data = default) where T : class;

}
