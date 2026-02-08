using TechStore.Api.Dtos.Clientes;
using TechStore.Core.Dtos;

namespace TechStore.Api.Mappers;

public static class EnderecoMapper
{
    public static CriarEnderecoDto ToCore(this CriarEnderecoRequest req) =>
        new(
            req.Descricao,
            req.Telefone,
            req.Cep,
            req.Logradouro,
            req.Numero,
            req.Complemento,
            req.Bairro,
            req.Cidade,
            req.Estado,
            req.Pais,
            req.IsDefaultShipping,
            req.IsDefaultBilling
        );
}
