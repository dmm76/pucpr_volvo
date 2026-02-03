using TechStore.Api.Security;
using TechStore.Core.Entities;

namespace TechStore.Infra.Fake.Repositories;

public class AdminRepositoryFake
{
    // admin seedado ao subir a API
    private readonly Admin _admin = new()
    {
        Login = "admin",
        Email = "admin@techstore.com",
        SenhaHash = HashService.GerarHash("Admin@123"),
    };

    public Admin? BuscarPorLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;

        return string.Equals(_admin.Login, login, StringComparison.OrdinalIgnoreCase)
            ? _admin
            : null;
    }
}
