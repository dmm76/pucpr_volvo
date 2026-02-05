namespace TechStore.Core.Dtos;

public record ProdutoDto(
    int Id,
    string Nome,
    string? Descricao,
    decimal Preco,
    int Estoque,
    int CategoriaId
);
