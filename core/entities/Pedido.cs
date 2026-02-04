using TechStore.Core.Enums;
using TechStore.Core.Exceptions;

namespace TechStore.Core.Entities;

public class Pedido
{
    public int Id { get; private set; }

    public StatusPedido Status { get; private set; } = StatusPedido.Carrinho;

    public int? ClienteId { get; private set; }
    public string? CustomerNameSnapshot { get; private set; }
    public string? ShippingAddressSnapshot { get; private set; }

    public FormaPagamento? PaymentMethod { get; private set; }

    public decimal Total { get; private set; }

    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; private set; } = DateTime.UtcNow;
    public DateTime? DataConfirmacao { get; private set; }
    public DateTime? DataPagamento { get; private set; }

    private readonly List<ItemPedido> _itens = new();
    public IReadOnlyCollection<ItemPedido> Itens => _itens;

    protected Pedido() { }

    public void MarcarAtualizacao() => DataAtualizacao = DateTime.UtcNow;

    // 1) Adiciona item (se já existe o mesmo ProdutoId, soma quantidade)
    public void AdicionarItem(Produto produto, int quantidade)
    {
        GarantirStatus(StatusPedido.Carrinho);

        if (produto is null)
            throw new BusinessRuleException(ErrorCodes.OrderItemProductRequired);

        if (!produto.Ativo)
            throw new BusinessRuleException(ErrorCodes.ProductInactive);

        if (quantidade <= 0)
            throw new BusinessRuleException(ErrorCodes.OrderItemQuantityInvalid);

        // valida estoque no momento de adicionar ao carrinho
        if (produto.Estoque < quantidade)
            throw new BusinessRuleException(ErrorCodes.OrderItemInsufficientStock);

        var existente = _itens.FirstOrDefault(i => i.ProdutoId == produto.Id);

        if (existente is null)
        {
            // cria com snapshot (nome + preco atual)
            var novo = new ItemPedido(pedidoId: 0, produto: produto, quantidade: quantidade);
            // pedidoId pode ser setado pelo EF depois; 0 é ok aqui
            _itens.Add(novo);
        }
        else
        {
            // soma e recalcula subtotal dentro do item
            existente.SomarQuantidade(quantidade);

            // revalida estoque total do item (o pedido pode ter acumulado)
            if (produto.Estoque < existente.Quantidade)
                throw new BusinessRuleException(ErrorCodes.OrderItemInsufficientStock);
        }

        RecalcularTotal();
        MarcarAtualizacao();
    }

    // 2) Remove item
    public void RemoverItem(int produtoId)
    {
        GarantirStatus(StatusPedido.Carrinho);

        var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);
        if (item is null)
            return;

        _itens.Remove(item);

        RecalcularTotal();
        MarcarAtualizacao();
    }

    // 3) Define/atualiza pagamento (só no carrinho)
    public void DefinirFormaPagamento(FormaPagamento paymentMethod)
    {
        GarantirStatus(StatusPedido.Carrinho);

        PaymentMethod = paymentMethod;
        MarcarAtualizacao();
    }

    // 4) Define endereço snapshot (só no carrinho)
    public void DefinirEnderecoEntregaSnapshot(string enderecoTexto)
    {
        GarantirStatus(StatusPedido.Carrinho);

        if (string.IsNullOrWhiteSpace(enderecoTexto))
            throw new BusinessRuleException(ErrorCodes.OrderShippingAddressRequired);

        ShippingAddressSnapshot = enderecoTexto.Trim();
        MarcarAtualizacao();
    }

    // 5) Identifica o cliente (na hora de fechar)
    public void IdentificarCliente(int clienteId, string customerNameSnapshot)
    {
        GarantirStatus(StatusPedido.Carrinho);

        if (clienteId <= 0)
            throw new BusinessRuleException(ErrorCodes.OrderCustomerRequired);

        if (string.IsNullOrWhiteSpace(customerNameSnapshot))
            throw new BusinessRuleException(ErrorCodes.OrderCustomerRequired);

        ClienteId = clienteId;
        CustomerNameSnapshot = customerNameSnapshot.Trim();
        MarcarAtualizacao();
    }

    // 6) Confirmar (Carrinho -> Pendente) - valida itens/cliente/endereço/pagamento
    public void Confirmar()
    {
        GarantirStatus(StatusPedido.Carrinho);

        if (_itens.Count == 0)
            throw new BusinessRuleException(ErrorCodes.OrderItemsRequired);

        if (ClienteId is null || string.IsNullOrWhiteSpace(CustomerNameSnapshot))
            throw new BusinessRuleException(ErrorCodes.OrderCustomerRequired);

        if (string.IsNullOrWhiteSpace(ShippingAddressSnapshot))
            throw new BusinessRuleException(ErrorCodes.OrderShippingAddressRequired);

        if (PaymentMethod is null)
            throw new BusinessRuleException(ErrorCodes.OrderPaymentMethodRequired);

        Status = StatusPedido.Pendente;
        DataConfirmacao = DateTime.UtcNow;

        RecalcularTotal();
        MarcarAtualizacao();
    }

    // 7) Marcar como pago (Pendente -> Pago) - valida estoque novamente e baixa estoque
    public void MarcarComoPago(IEnumerable<Produto> produtosDoPedido)
    {
        GarantirStatus(StatusPedido.Pendente);

        if (_itens.Count == 0)
            throw new BusinessRuleException(ErrorCodes.OrderItemsRequired);

        // valida e baixa estoque somente aqui (momento do pagamento)
        foreach (var item in _itens)
        {
            var produto = produtosDoPedido.FirstOrDefault(p => p.Id == item.ProdutoId);
            if (produto is null)
                throw new BusinessRuleException(ErrorCodes.ProductNotFound);

            if (!produto.Ativo)
                throw new BusinessRuleException(ErrorCodes.ProductInactive);

            // valida estoque real no momento do pagamento
            if (produto.Estoque < item.Quantidade)
                throw new BusinessRuleException(ErrorCodes.OrderItemInsufficientStock);
        }

        // baixa estoque
        foreach (var item in _itens)
        {
            var produto = produtosDoPedido.First(p => p.Id == item.ProdutoId);
            produto.ReduzirEstoque(item.Quantidade);
        }

        Status = StatusPedido.Pago;
        DataPagamento = DateTime.UtcNow;

        RecalcularTotal();
        MarcarAtualizacao();
    }

    private void RecalcularTotal()
    {
        Total = _itens.Sum(i => i.SubTotal);
    }

    private void GarantirStatus(StatusPedido esperado)
    {
        if (Status != esperado)
            throw new BusinessRuleException(ErrorCodes.OrderStatusInvalid);
    }
}
