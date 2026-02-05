namespace TechStore.Api.Dtos;

public record EnderecoRequest(
    string Descricao,
    string Telefone,
    string Cep,
    int CodIbge,
    double Latitude,
    double Longitude,
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

public record CadastrarClienteRequest(
    string Nome,
    string Telefone,
    string Email,
    string Login,
    string? Senha,
    string? DocumentoIdentidade,
    EnderecoRequest? Endereco
);
