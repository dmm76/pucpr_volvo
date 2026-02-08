using Microsoft.EntityFrameworkCore;
using TechStore.Core.Entities;
using TechStore.Core.Enums;
using TechStore.Core.Interfaces;
using TechStore.Infra.Context;

namespace TechStore.Infra.Sql.Repositories;

public class PedidoRepositorySql : IPedidoRepository
{
    private readonly TechStoreDbContext _ctx;

    public PedidoRepositorySql(TechStoreDbContext ctx) => _ctx = ctx;

    public Pedido? BuscarPorId(int id) =>
        _ctx.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.Id == id);

    public List<Pedido> BuscarTodos() => _ctx.Pedidos.AsNoTracking().ToList();

    public Pedido Inserir(Pedido pedido)
    {
        _ctx.Pedidos.Add(pedido);
        _ctx.SaveChanges();
        return pedido;
    }

    public void Atualizar(Pedido pedido)
    {
        _ctx.Pedidos.Update(pedido);
        _ctx.SaveChanges();
    }

    public void Remover(int id)
    {
        var entity = _ctx.Pedidos.FirstOrDefault(x => x.Id == id);
        if (entity is null)
            return;
        _ctx.Pedidos.Remove(entity);
        _ctx.SaveChanges();
    }

    public List<Pedido> BuscarPorCliente(int clienteId) =>
        _ctx.Pedidos.AsNoTracking().Where(x => x.ClienteId == clienteId).ToList();

    public Pedido? BuscarCarrinhoPorVisitorId(Guid visitorId) =>
        _ctx
            .Pedidos.Include(p => p.Itens)
            .FirstOrDefault(p => p.Status == StatusPedido.Carrinho && p.VisitorId == visitorId);
}
