using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos.Categoria;
using TechStore.Api.Security;
using TechStore.Core.useCases.categorias;

namespace TechStore.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly CategoriaUseCases _service;
    private readonly AuthState _auth;

    public CategoriasController(CategoriaUseCases service, AuthState auth)
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
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CriarCategoriaRequest request)
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        if (request is null)
            return BadRequest(new { message = "Body é obrigatório." });

        var dto = _service.Criar(request.Nome, request.Descricao);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] AtualizarCategoriaRequest request)
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        if (request is null)
            return BadRequest(new { message = "Body é obrigatório." });

        var dto = _service.Atualizar(id, request.Nome, request.Descricao);
        return Ok(dto);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        _service.Remover(id);
        return NoContent();
    }
}
