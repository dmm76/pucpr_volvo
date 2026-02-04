using TechStore.Core.Exceptions;

namespace TechStore.Core.Entities;

public class Produto
{
    public int Id { get; private set; }

    public int CategoriaId { get; private set; }

    public string Nome { get; private set; } = "";
    public string? Descricao { get; private set; }

    public decimal PrecoAtual { get; private set; }
    public int Estoque { get; private set; }

    public bool Ativo { get; private set; } = true;

    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; private set; } = DateTime.UtcNow;

    protected Produto() { }

    public Produto(
        int categoriaId,
        string nome,
        decimal precoAtual,
        int estoque,
        string? descricao = null
    )
    {
        DefinirCategoria(categoriaId);
        AtualizarNome(nome);
        AtualizarDescricao(descricao);
        AtualizarPreco(precoAtual);
        AjustarEstoque(estoque);
    }

    public void MarcarAtualizacao() => DataAtualizacao = DateTime.UtcNow;

    public void DefinirCategoria(int categoriaId)
    {
        if (categoriaId <= 0)
            throw new BusinessRuleException(ErrorCodes.ProductCategoryInvalid);
        CategoriaId = categoriaId;
        MarcarAtualizacao();
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BusinessRuleException(ErrorCodes.ProductNameRequired);

        var n = nome.Trim();

        if (n.Length < 2 || n.Length > 120)
            throw new BusinessRuleException(ErrorCodes.ProductNameInvalidLength);

        if (Nome == n)
            return;

        Nome = n;
        MarcarAtualizacao();
    }

    public void AtualizarDescricao(string? descricao)
    {
        Descricao = string.IsNullOrWhiteSpace(descricao) ? null : descricao.Trim();
        MarcarAtualizacao();
    }

    public void AtualizarPreco(decimal preco)
    {
        if (preco <= 0)
            throw new BusinessRuleException(ErrorCodes.ProductPriceInvalid);
        PrecoAtual = preco;
        MarcarAtualizacao();
    }

    public void ReduzirEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new BusinessRuleException(ErrorCodes.OrderItemQuantityInvalid);

        if (Estoque < quantidade)
            throw new BusinessRuleException(ErrorCodes.OrderItemInsufficientStock);

        Estoque -= quantidade;
        MarcarAtualizacao();
    }

    public void AumentarEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new BusinessRuleException(ErrorCodes.OrderItemQuantityInvalid);

        Estoque += quantidade;
        MarcarAtualizacao();
    }

    public void AjustarEstoque(int estoque)
    {
        if (estoque < 0)
            throw new BusinessRuleException(ErrorCodes.ProductStockInvalid);
        Estoque = estoque;
        MarcarAtualizacao();
    }

    private void GarantirAtivo()
    {
        if (!Ativo)
            throw new BusinessRuleException(ErrorCodes.ProductInactive);
    }

    public void Desativar()
    {
        Ativo = false;
        MarcarAtualizacao();
    }

    public void Ativar()
    {
        Ativo = true;
        MarcarAtualizacao();
    }
}
