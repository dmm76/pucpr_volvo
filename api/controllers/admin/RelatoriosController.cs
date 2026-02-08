using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Security;
using TechStore.Core.UseCases.Relatorios;

namespace TechStore.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/relatorios")]
public class RelatoriosController : ControllerBase
{
    private readonly RelatorioUseCases _useCases;
    private readonly AuthState _auth;

    public RelatoriosController(RelatorioUseCases useCases, AuthState auth)
    {
        _useCases = useCases;
        _auth = auth;
    }

    [HttpGet("vendas-por-categoria")]
    public IActionResult VendasPorCategoria()
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        var dto = _useCases.VendasPorCategoria();

        if (dto.Count == 0)
            return NoContent();

        return Ok(dto);
    }
}
