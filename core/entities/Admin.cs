namespace TechStore.Core.Entities;

public class Admin
{
    public string Login { get; init; } = "";
    public string Email { get; init; } = "";
    public string SenhaHash { get; init; } = "";
}
