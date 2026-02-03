namespace TechStore.Api.Auth;

public class AuthState
{
    public bool AdminLogado { get; private set; }
    public string? AdminLogin { get; private set; }
    public string? AdminEmail { get; private set; }

    public void Logar(string login, string email)
    {
        AdminLogado = true;
        AdminLogin = login;
        AdminEmail = email;
    }

    public void Logout()
    {
        AdminLogado = false;
        AdminLogin = null;
        AdminEmail = null;
    }
}
