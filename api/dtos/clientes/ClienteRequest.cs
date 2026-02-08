namespace TechStore.Api.Dtos.Clientes;

public record CadastrarClienteRequest(
    string Nome,
    string Telefone,
    string Email,
    string Login,
    string? Senha,
    string? DocumentoIdentidade
);

public record AtualizarClienteRequest(string? Nome, string? Telefone, string? DocumentoIdentidade);
