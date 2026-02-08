namespace TechStore.Api.Dtos.Produtos;

public class BuscarProdutosQuery
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;

    public string? Nome { get; set; }
    public decimal? PrecoMin { get; set; }
    public decimal? PrecoMax { get; set; }
}
