using TechStore.Api.Security;
using TechStore.Core.Entities;

namespace TechStore.Infra.Fake.Repositories;

public class UserRepositoryFake
{
    // usuario admin seedado ao subir a API
    private readonly User _admin = new()
    {
        Login = "admin",
        Email = "admin@techstore.com",
        SenhaHash = HashService.GerarHash("Admin@123"),
        Role = UserRole.Admin,
    };

    public User? BuscarPorLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;

        return string.Equals(_admin.Login, login, StringComparison.OrdinalIgnoreCase)
            ? _admin
            : null;
    }
}
