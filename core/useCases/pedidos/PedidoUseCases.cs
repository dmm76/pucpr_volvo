using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Interfaces;

namespace TechStore.Core.UseCases.Pedidos;

public class PedidoUseCases
{
    private readonly IPedidoRepository _pedidoRepo;
    private readonly IProdutoRepository _produtoRepo;

    public PedidoUseCases(IPedidoRepository pedidoRepo, IProdutoRepository produtoRepo)
    {
        _pedidoRepo = pedidoRepo;
        _produtoRepo = produtoRepo;
    }

    public PedidoDetalheDto CriarCarrinho()
    {
        var pedido = Pedido.CriarCarrinho();
        _pedidoRepo.Inserir(pedido);
        return Map(pedido);
    }

    public PedidoDetalheDto BuscarPorId(int pedidoId)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        return Map(pedido);
    }

    public IReadOnlyList<PedidoDetalheDto> BuscarPorCliente(int clienteId) =>
        _pedidoRepo.BuscarPorCliente(clienteId).Select(Map).ToList();

    public PedidoDetalheDto AdicionarItem(int pedidoId, int produtoId, int quantidade)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        var produto =
            _produtoRepo.BuscarPorId(produtoId)
            ?? throw new NotFoundException(ErrorCodes.ProductNotFound);

        pedido.AdicionarItem(produto, quantidade);
        _pedidoRepo.Atualizar(pedido);

        return Map(pedido);
    }

    public int? BuscarClienteIdDoPedido(int pedidoId)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        return pedido.ClienteId;
    }

    public PedidoDetalheDto RemoverItem(int pedidoId, int produtoId)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        pedido.RemoverItem(produtoId);
        _pedidoRepo.Atualizar(pedido);

        return Map(pedido);
    }

    public PedidoDetalheDto DefinirEnderecoEntrega(int pedidoId, string endereco)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        pedido.DefinirEnderecoEntregaSnapshot(endereco);
        _pedidoRepo.Atualizar(pedido);

        return Map(pedido);
    }

    public PedidoDetalheDto DefinirFormaPagamento(int pedidoId, FormaPagamento formaPagamento)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        pedido.DefinirFormaPagamento(formaPagamento);
        _pedidoRepo.Atualizar(pedido);

        return Map(pedido);
    }

    public PedidoDetalheDto IdentificarCliente(
        int pedidoId,
        int clienteId,
        string customerNameSnapshot
    )
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        pedido.IdentificarCliente(clienteId, customerNameSnapshot);
        _pedidoRepo.Atualizar(pedido);

        return Map(pedido);
    }

    public PedidoDetalheDto Confirmar(int pedidoId)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        pedido.Confirmar();
        _pedidoRepo.Atualizar(pedido);

        return Map(pedido);
    }

    public PedidoDetalheDto Pagar(int pedidoId)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        // Carrega produtos reais do pedido para validação/baixa de estoque
        var produtos = pedido
            .Itens.Select(i =>
                _produtoRepo.BuscarPorId(i.ProdutoId)
                ?? throw new NotFoundException(ErrorCodes.ProductNotFound)
            )
            .ToList();

        pedido.MarcarComoPago(produtos);

        // persiste estoque atualizado
        foreach (var p in produtos)
            _produtoRepo.Atualizar(p);

        _pedidoRepo.Atualizar(pedido);

        return Map(pedido);
    }

    private static PedidoDetalheDto Map(Pedido p) =>
        new(
            p.Id,
            p.Status,
            p.ClienteId,
            p.CustomerNameSnapshot,
            p.ShippingAddressSnapshot,
            p.PaymentMethod,
            p.Total,
            p.Itens.Select(i => new ItemPedidoDto(
                    i.ProdutoId,
                    i.NomeProdutoSnapshot,
                    i.PrecoUnitarioSnapshot,
                    i.Quantidade,
                    i.SubTotal
                ))
                .ToList()
        );
}
