namespace Application.DTOs
{
    public record LoginResponse(UserDto User, string Token);

    public record UserDto(int Id, string Name, string Email, List<string> Roles);
}
