using TechStore.Core.Entities;
using TechStore.Core.Interfaces;

namespace TechStore.Infra.Fake.Repositories;

public class UserRepositoryFake
{
    private readonly User _admin;

    public UserRepositoryFake(IPasswordHasher hasher)
    {
        _admin = new User(
            login: "admin",
            email: "admin@techstore.com",
            senhaHash: hasher.Hash("Admin@123"),
            role: UserRole.Admin
        );
    }

    public User? BuscarPorLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;

        return string.Equals(_admin.Login, login, StringComparison.OrdinalIgnoreCase)
            ? _admin
            : null;
    }
}
