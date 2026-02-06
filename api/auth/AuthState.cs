using TechStore.Core.Entities;

namespace TechStore.Api.Auth;

public class AuthState
{
    public bool UserLogado { get; private set; }
    public int? UserId { get; private set; }
    public int? ClienteId { get; private set; }
    public string? UserLogin { get; private set; }
    public string? UserEmail { get; private set; }
    public UserRole? UserRole { get; private set; }

    public void Logar(int userId, string login, string email, UserRole role, int? clienteId = null)
    {
        UserLogado = true;
        UserId = userId;
        ClienteId = clienteId;

        UserLogin = login;
        UserEmail = email;
        UserRole = role;
    }

    public void Logout()
    {
        UserLogado = false;

        UserId = null;
        ClienteId = null;

        UserLogin = null;
        UserEmail = null;
        UserRole = null;
    }
}
