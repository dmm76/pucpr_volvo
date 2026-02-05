namespace TechStore.Core.Dtos;

public record EnderecoDto(
    int Id,
    string Descricao,
    string Telefone,
    string Cep,
    string Logradouro,
    int Numero,
    string Complemento,
    string Bairro,
    string Cidade,
    string Estado,
    string Pais,
    bool IsDefaultShipping,
    bool IsDefaultBilling
);

public record ClienteDetalheDto(
    int Id,
    int UserId,
    string Login,
    string Email,
    string Nome,
    string Telefone,
    string? DocumentoIdentidade,
    IReadOnlyList<EnderecoDto> Enderecos,
    string? SenhaTemporaria // só quando gerar automaticamente
);
