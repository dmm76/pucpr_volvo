using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos.Clientes;
using TechStore.Api.Mappers;
using TechStore.Api.Security;
using TechStore.Core.Dtos;
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
        if (req is null)
            return BadRequest(new { message = "Body é obrigatório." });

        // ainda compatível com UseCase atual (que recebe Endereco?):
        var dto = _useCases.Cadastrar(
            nome: req.Nome,
            telefone: req.Telefone,
            email: req.Email,
            login: req.Login,
            senha: req.Senha,
            documentoIdentidade: req.DocumentoIdentidade,
            enderecoOpcional: null
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

    [HttpGet("me")]
    public IActionResult Me()
    {
        var block = UserGuard.BloquearSeNaoLogado(_auth);
        if (block is not null)
            return block;

        if (_auth.ClienteId is null)
            return new ObjectResult(new { message = "Usuario nao possui cliente associado." })
            {
                StatusCode = 403,
            };

        return Ok(_useCases.BuscarPorId(_auth.ClienteId.Value));
    }

    [HttpPost("me/enderecos")]
    public IActionResult AdicionarEndereco([FromBody] CriarEnderecoRequest req)
    {
        var block = UserGuard.BloquearSeNaoLogado(_auth);
        if (block is not null)
            return block;

        if (_auth.UserId is null)
            return new ObjectResult(new { message = "Usuario logado invalido." })
            {
                StatusCode = 403,
            };

        if (req is null)
            return BadRequest(new { message = "Body é obrigatório." });

        var dto = _useCases.AdicionarEnderecoMe(userId: _auth.UserId.Value, req: req.ToCore());

        return Ok(dto);
    }
}
