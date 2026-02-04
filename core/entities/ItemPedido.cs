using TechStore.Core.Exceptions;

namespace TechStore.Core.Entities;

public class ItemPedido
{
    public int Id { get; private set; }

    public int PedidoId { get; private set; }
    public int ProdutoId { get; private set; }

    public string NomeProdutoSnapshot { get; private set; } = "";
    public decimal PrecoUnitarioSnapshot { get; private set; }

    public int Quantidade { get; private set; }
    public decimal SubTotal { get; private set; }

    protected ItemPedido() { }

    public ItemPedido(Produto produto, int quantidade)
    {
        if (produto is null)
            throw new BusinessRuleException(ErrorCodes.OrderItemProductRequired);

        if (produto.PrecoAtual <= 0)
            throw new BusinessRuleException(ErrorCodes.ProductPriceInvalid);

        ProdutoId = produto.Id;
        NomeProdutoSnapshot = produto.Nome;
        PrecoUnitarioSnapshot = produto.PrecoAtual;

        AtualizarQuantidade(quantidade);
    }

    public void AtualizarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new BusinessRuleException(ErrorCodes.OrderItemQuantityInvalid);

        Quantidade = quantidade;
        RecalcularSubtotal();
    }

    public void SomarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new BusinessRuleException(ErrorCodes.OrderItemQuantityInvalid);

        Quantidade += quantidade;
        RecalcularSubtotal();
    }

    private void RecalcularSubtotal()
    {
        SubTotal = PrecoUnitarioSnapshot * Quantidade;
    }
}
