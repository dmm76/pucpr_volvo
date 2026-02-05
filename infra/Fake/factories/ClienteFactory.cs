using TechStore.Core.Entities;

namespace TechStore.Infra.Fake.Factories;

public static class ClienteFactory
{
    // users PRECISAM ter Id (ou seja: já inseridos no UserRepositoryFake)
    public static List<Cliente> Criar(List<User> users) =>
        new()
        {
            new Cliente(users[0].Id, "Douglas Marcelo Monquero", "44999990000", "123.456.789-00"),
            new Cliente(users[1].Id, "Ana Beatriz", "44999991111", "987.654.321-00"),
            new Cliente(users[2].Id, "Carlos Silva", "44999992222", "111.222.333-44"),
        };
}
