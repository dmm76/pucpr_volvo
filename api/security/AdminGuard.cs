using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Core.Entities;

namespace TechStore.Api.Security;

public static class AdminGuard
{
    public static IActionResult? BloquearSeNaoLogado(AuthState auth)
    {
        if (!auth.UserLogado)
            return new UnauthorizedObjectResult(new { message = "Usuario precisa estar logado." });

        if (auth.UserRole != UserRole.Admin)
            return new ObjectResult(new { message = "Usuario precisa ser admin." })
            {
                StatusCode = 403,
            };

        return null;
    }
}
