using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Dtos;
using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.UseCases.Clientes;

namespace TechStore.Api.Controllers.Public;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly ClienteUseCases _useCases;

    public ClientesController(ClienteUseCases useCases)
    {
        _useCases = useCases;
    }

    [HttpPost]
    public ActionResult<ClienteDetalheDto> Cadastrar([FromBody] CadastrarClienteRequest req)
    {
        Endereco? endereco = null;

        if (req.Endereco is not null)
        {
            // clienteId ainda não existe aqui -> vamos criar o endereço com clienteId=0
            // e depois o domínio guarda dentro do cliente (fake). No EF, isso vira 1-N correto.
            endereco = new Endereco(
                clienteId: 0,
                descricao: req.Endereco.Descricao,
                telefone: req.Endereco.Telefone,
                cep: req.Endereco.Cep,
                codIbge: req.Endereco.CodIbge,
                latitude: req.Endereco.Latitude,
                longitude: req.Endereco.Longitude,
                logradouro: req.Endereco.Logradouro,
                numero: req.Endereco.Numero,
                complemento: req.Endereco.Complemento,
                bairro: req.Endereco.Bairro,
                cidade: req.Endereco.Cidade,
                estado: req.Endereco.Estado,
                pais: req.Endereco.Pais,
                isDefaultShipping: req.Endereco.IsDefaultShipping,
                isDefaultBilling: req.Endereco.IsDefaultBilling
            );
        }

        var dto = _useCases.Cadastrar(
            nome: req.Nome,
            telefone: req.Telefone,
            email: req.Email,
            login: req.Login,
            senha: req.Senha,
            documentoIdentidade: req.DocumentoIdentidade,
            enderecoOpcional: endereco
        );

        return CreatedAtAction(nameof(BuscarPorId), new { id = dto.Id }, dto);
    }

    [HttpGet("{id:int}")]
    public ActionResult<ClienteDetalheDto> BuscarPorId(int id) => Ok(_useCases.BuscarPorId(id));

    [HttpGet]
    public ActionResult<IReadOnlyList<ClienteDetalheDto>> BuscarTodos() =>
        Ok(_useCases.BuscarTodos());
}
