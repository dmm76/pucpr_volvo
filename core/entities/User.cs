namespace TechStore.Core.Entities;

public class User
{
    public string Login { get; init; } = "";
    public string Email { get; init; } = "";
    public string SenhaHash { get; init; } = "";
    public UserRole Role { get; init; } = UserRole.Usuario;
}
