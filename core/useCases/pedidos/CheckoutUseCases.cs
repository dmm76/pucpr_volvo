using TechStore.Core.Dtos;
using TechStore.Core.Exceptions;
using TechStore.Core.Interfaces;

namespace TechStore.Core.UseCases.Pedidos;

public class CheckoutUseCases
{
    private readonly IPedidoRepository _pedidoRepo;
    private readonly IClienteRepository _clienteRepo;

    public CheckoutUseCases(IPedidoRepository pedidoRepo, IClienteRepository clienteRepo)
    {
        _pedidoRepo = pedidoRepo;
        _clienteRepo = clienteRepo;
    }

    public PedidoDetalheDto UsarEnderecoPadraoEntrega(int pedidoId, int clienteId)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        var cliente =
            _clienteRepo.BuscarPorId(clienteId)
            ?? throw new NotFoundException(ErrorCodes.ClienteNotFound);

        var endereco =
            cliente.Enderecos.FirstOrDefault(e => e.IsDefaultShipping)
            ?? throw new BusinessRuleException(ErrorCodes.ClienteDefaultShippingAddressNotFound);

        var snapshot =
            $"{endereco.Descricao} - {endereco.Logradouro}, {endereco.Numero} {endereco.Complemento}, "
            + $"{endereco.Bairro}, {endereco.Cidade}-{endereco.Estado}, CEP {endereco.CEP}, {endereco.Pais}";

        pedido.DefinirEnderecoEntregaSnapshot(snapshot);
        _pedidoRepo.Atualizar(pedido);

        // ✅ evita duplicar Map: reaproveita o BuscarPorId do repositório e monta aqui simples
        // (ou você move o "Map" para um mapper helper depois)
        return new PedidoDetalheDto(
            pedido.Id,
            pedido.Status,
            pedido.ClienteId,
            pedido.CustomerNameSnapshot,
            pedido.ShippingAddressSnapshot,
            pedido.PaymentMethod,
            pedido.Total,
            pedido
                .Itens.Select(i => new ItemPedidoDto(
                    i.ProdutoId,
                    i.NomeProdutoSnapshot,
                    i.PrecoUnitarioSnapshot,
                    i.Quantidade,
                    i.SubTotal
                ))
                .ToList()
        );
    }

    public PedidoDetalheDto IdentificarClienteAutoSnapshot(int pedidoId, int clienteId)
    {
        var pedido =
            _pedidoRepo.BuscarPorId(pedidoId)
            ?? throw new NotFoundException(ErrorCodes.OrderNotFound);

        var cliente =
            _clienteRepo.BuscarPorId(clienteId)
            ?? throw new NotFoundException(ErrorCodes.ClienteNotFound);

        // snapshot vem do cadastro e fica congelado no pedido
        pedido.IdentificarCliente(cliente.Id, cliente.Nome);
        _pedidoRepo.Atualizar(pedido);

        return new PedidoDetalheDto(
            pedido.Id,
            pedido.Status,
            pedido.ClienteId,
            pedido.CustomerNameSnapshot,
            pedido.ShippingAddressSnapshot,
            pedido.PaymentMethod,
            pedido.Total,
            pedido
                .Itens.Select(i => new ItemPedidoDto(
                    i.ProdutoId,
                    i.NomeProdutoSnapshot,
                    i.PrecoUnitarioSnapshot,
                    i.Quantidade,
                    i.SubTotal
                ))
                .ToList()
        );
    }
}
