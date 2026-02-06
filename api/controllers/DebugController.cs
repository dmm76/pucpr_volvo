#if DEBUG
using Microsoft.AspNetCore.Mvc;
using TechStore.Core.Exceptions;

namespace TechStore.Api.Controllers;

[ApiController]
[Route("api/debug")]
public class DebugController : ControllerBase
{
    [HttpGet("erro-desconhecido")]
    public IActionResult ErroDesconhecido()
    {
        throw new BusinessRuleException("UNKNOWN_TEST_CODE");
    }

    [HttpGet("erro-500")]
    public IActionResult Erro500()
    {
        throw new Exception("boom");
    }
}
#endif
