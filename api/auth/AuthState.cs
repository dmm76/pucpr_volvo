using TechStore.Core.Entities;

namespace TechStore.Api.Auth;

public class AuthState
{
    public bool UserLogado { get; private set; }
    public string? UserLogin { get; private set; }
    public string? UserEmail { get; private set; }
    public UserRole? UserRole { get; private set; }

    public void Logar(string login, string email, UserRole role)
    {
        UserLogado = true;
        UserLogin = login;
        UserEmail = email;
        UserRole = role;
    }

    public void Logout()
    {
        UserLogado = false;
        UserLogin = null;
        UserEmail = null;
        UserRole = null;
    }
}
