namespace TechStore.Api.Dtos.Clientes;

public record CriarEnderecoRequest(
    string Descricao,
    string Telefone,
    string Cep,
    string Logradouro,
    int Numero,
    string? Complemento,
    string Bairro,
    string Cidade,
    string Estado,
    string Pais,
    bool IsDefaultShipping,
    bool IsDefaultBilling
);
