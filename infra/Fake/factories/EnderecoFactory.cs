using TechStore.Core.Entities;

namespace TechStore.Infra.Fake.Factories;

public static class EnderecoFactory
{
    public static List<Endereco> CriarParaCliente(Cliente cliente)
    {
        // Cliente PRECISA ter Id (ou seja: já inserido no ClienteRepositoryFake)
        return new List<Endereco>
        {
            new Endereco(
                clienteId: cliente.Id,
                descricao: "Casa",
                telefone: cliente.Telefone,
                cep: "87000-000",
                codIbge: 4115200,
                latitude: -23.4253,
                longitude: -51.9386,
                logradouro: "Rua Exemplo",
                numero: 123,
                complemento: "Apto 12",
                bairro: "Centro",
                cidade: "Maringá",
                estado: "PR",
                pais: "Brasil",
                isDefaultShipping: true,
                isDefaultBilling: true
            ),
            new Endereco(
                clienteId: cliente.Id,
                descricao: "Trabalho",
                telefone: cliente.Telefone,
                cep: "87010-010",
                codIbge: 4115200,
                latitude: -23.4200,
                longitude: -51.9300,
                logradouro: "Av. Brasil",
                numero: 999,
                complemento: "Sala 05",
                bairro: "Zona 01",
                cidade: "Maringá",
                estado: "PR",
                pais: "Brasil",
                isDefaultShipping: false,
                isDefaultBilling: false
            ),
        };
    }
}
