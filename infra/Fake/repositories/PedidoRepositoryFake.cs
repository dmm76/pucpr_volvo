using TechStore.Core.Entities;
using TechStore.Core.Interfaces;

namespace TechStore.Infra.Fake.Repositories;

public class PedidoRepositoryFake : IPedidoRepository
{
    private readonly List<Pedido> _data = new();
    private int _nextId = 0;

    public Pedido? BuscarPorId(int id) => _data.FirstOrDefault(x => x.Id == id);

    public List<Pedido> BuscarTodos() => _data.ToList();

    public Pedido Inserir(Pedido pedido)
    {
        var id = Interlocked.Increment(ref _nextId);
        FakeEntitySetter.SetPrivateId(pedido, id);

        _data.Add(pedido);
        return pedido;
    }

    public void Atualizar(Pedido pedido)
    {
        var idx = _data.FindIndex(x => x.Id == pedido.Id);
        if (idx < 0)
            return;

        _data[idx] = pedido;
    }

    public void Remover(int id)
    {
        var p = BuscarPorId(id);
        if (p is null)
            return;

        _data.Remove(p);
    }

    public List<Pedido> BuscarPorCliente(int clienteId) =>
        _data.Where(x => x.ClienteId == clienteId).ToList();
}
