namespace TechStore.Api.Dtos.Categoria;

// Create: Nome obrigatório
public record CriarCategoriaRequest(string Nome, string? Descricao);

// Update: campos opcionais (null = manter)
public record AtualizarCategoriaRequest(string? Nome, string? Descricao);
