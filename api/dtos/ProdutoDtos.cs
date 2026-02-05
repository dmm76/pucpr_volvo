namespace TechStore.Api.Dtos;

// Requests (entrada)
public record CriarProdutoRequest(
    int CategoriaId,
    string Nome,
    string? Descricao,
    decimal Preco,
    int Estoque
);

public record AtualizarProdutoRequest(
    int CategoriaId,
    string Nome,
    string? Descricao,
    decimal Preco,
    int Estoque
);
