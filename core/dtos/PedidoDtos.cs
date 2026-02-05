using TechStore.Core.Enums;

namespace TechStore.Core.Dtos;

public record ItemPedidoDto(
    int ProdutoId,
    string NomeProdutoSnapshot,
    decimal PrecoUnitarioSnapshot,
    int Quantidade,
    decimal SubTotal
);

public record PedidoDetalheDto(
    int Id,
    StatusPedido Status,
    int? ClienteId,
    string? CustomerNameSnapshot,
    string? ShippingAddressSnapshot,
    FormaPagamento? PaymentMethod,
    decimal Total,
    IReadOnlyList<ItemPedidoDto> Itens
);
