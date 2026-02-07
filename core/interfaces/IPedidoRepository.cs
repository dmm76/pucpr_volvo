using TechStore.Core.Entities;

namespace TechStore.Core.Interfaces;

public interface IPedidoRepository
{
    Pedido? BuscarPorId(int id);
    List<Pedido> BuscarTodos();

    Pedido Inserir(Pedido pedido);
    void Atualizar(Pedido pedido);
    void Remover(int id);

    List<Pedido> BuscarPorCliente(int clienteId);

    //evitar pedidos infinitos - muito importante para evitar pedidos fantasmas
    Pedido? BuscarCarrinhoPorVisitorId(Guid visitorId);
}
