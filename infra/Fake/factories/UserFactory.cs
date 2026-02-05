using TechStore.Core.Entities;
using TechStore.Core.Interfaces;

namespace TechStore.Infra.Fake.Factories;

public static class UserFactory
{
    // Não seta Id aqui. O UserRepositoryFake já faz isso no Inserir().
    public static List<User> CriarClientes(IPasswordHasher hasher) =>
        new()
        {
            new User(
                login: "douglas",
                email: "douglas@techstore.com",
                senhaHash: hasher.Hash("Douglas@123"),
                role: UserRole.Usuario
            ),
            new User(
                login: "ana",
                email: "ana@techstore.com",
                senhaHash: hasher.Hash("Ana@123"),
                role: UserRole.Usuario
            ),
            new User(
                login: "carlos",
                email: "carlos@techstore.com",
                senhaHash: hasher.Hash("Carlos@123"),
                role: UserRole.Usuario
            ),
        };
}
