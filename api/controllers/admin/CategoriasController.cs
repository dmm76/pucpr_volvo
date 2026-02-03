using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Security;
using TechStore.Core.Services;

namespace TechStore.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly CategoriaService _service;
    private readonly AuthState _auth;

    public CategoriasController(CategoriaService service, AuthState auth)
    {
        _service = service;
        _auth = auth;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        return Ok(_service.Listar());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        var dto = _service.BuscarPorId(id);
        return dto is null ? NotFound(new { message = "Categoria não encontrada", id }) : Ok(dto);
    }

    //request Dto - somente para receber nome da webapi
    public record CriarCategoriaRequest(string Nome);

    [HttpPost]
    public IActionResult Create([FromBody] CriarCategoriaRequest request)
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        var dto = _service.Criar(request.Nome);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }
}
