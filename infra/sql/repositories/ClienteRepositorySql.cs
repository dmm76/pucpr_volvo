using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Fake;

namespace TechStore.Infra.Sql.Repositories;

public class ClienteRepositorySql : IClienteRepository
{
    private readonly List<Cliente> _data = new();
    private int _nextId = 0;

    public Cliente? BuscarPorId(int id) => _data.FirstOrDefault(x => x.Id == id);

    public Cliente? BuscarPorUserId(int userId) => _data.FirstOrDefault(x => x.UserId == userId);

    public List<Cliente> BuscarTodos() => _data.ToList();

    public Cliente Inserir(Cliente cliente)
    {
        var id = Interlocked.Increment(ref _nextId);
        FakeEntitySetter.SetPrivateId(cliente, id);

        _data.Add(cliente);
        return cliente;
    }

    public void Atualizar(Cliente cliente)
    {
        var idx = _data.FindIndex(x => x.Id == cliente.Id);
        if (idx < 0)
            return;

        _data[idx] = cliente;
    }

    public void Remover(int id)
    {
        var c = BuscarPorId(id);
        if (c is null)
            return;

        _data.Remove(c);
    }

    public bool ExistePorUserId(int userId) => _data.Any(x => x.UserId == userId);
}
