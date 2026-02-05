using TechStore.Core.Entities;

namespace TechStore.Core.Interfaces;

public interface IClienteRepository
{
    Cliente? BuscarPorId(int id);
    List<Cliente> BuscarTodos();

    Cliente Inserir(Cliente cliente);
    void Atualizar(Cliente cliente);
    void Remover(int id);

    bool ExistePorUserId(int userId);
}
