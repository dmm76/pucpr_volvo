namespace TechStore.Core.Dtos;

public record CriarEnderecoDto(
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
