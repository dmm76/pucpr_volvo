using TechStore.Core.Interfaces;
using TechStore.Infra.Fake.Factories;

namespace TechStore.Infra.Fake.Seed;

public static class FakeSeedClientes
{
    public static void Seed(
        IUserRepository userRepo,
        IClienteRepository clienteRepo,
        IPasswordHasher hasher
    )
    {
        // 1) cria users e insere -> ganham Id
        var users = UserFactory.CriarClientes(hasher);
        foreach (var u in users)
            userRepo.Inserir(u);

        // 2) cria clientes com user.Id e insere -> ganham Id
        var clientes = ClienteFactory.Criar(users);
        foreach (var c in clientes)
            clienteRepo.Inserir(c);

        // 3) cria endereços usando cliente.Id e adiciona no aggregate
        foreach (var c in clientes)
        {
            var enderecos = EnderecoFactory.CriarParaCliente(c);
            foreach (var e in enderecos)
                c.AdicionarEndereco(e);

            // 4) persiste atualização do aggregate (endereços)
            clienteRepo.Atualizar(c);
        }
    }
}
