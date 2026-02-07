using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos;
using TechStore.Api.Security;
using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.UseCases.Clientes;

namespace TechStore.Api.Controllers.Public;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly AuthState _auth;
    private readonly ClienteUseCases _useCases;

    public ClientesController(AuthState auth, ClienteUseCases useCases)
    {
        _auth = auth;
        _useCases = useCases;
    }

    [HttpPost]
    public ActionResult<ClienteDetalheDto> Cadastrar([FromBody] CadastrarClienteRequest req)
    {
        Endereco? endereco = null;

        if (req.Endereco is not null)
        {
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

        return CreatedAtAction(nameof(BuscarPorId), new { clienteId = dto.Id }, dto);
    }

    [HttpGet]
    public IActionResult BuscarTodos()
    {
        var block = AdminGuard.BloquearSeNaoLogado(_auth);
        if (block is not null)
            return block;

        return Ok(_useCases.BuscarTodos());
    }

    [HttpGet("{clienteId:int}")]
    public IActionResult BuscarPorId(int clienteId)
    {
        var block = OwnershipGuard.BloquearSeNaoDonoOuAdmin(_auth, clienteId);
        if (block is not null)
            return block;

        return Ok(_useCases.BuscarPorId(clienteId));
    }
}
