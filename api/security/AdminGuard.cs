using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;

namespace TechStore.Api.Security;

public static class AdminGuard
{
    public static IActionResult? BloquearSeNaoLogado(AuthState auth)
    {
        if (!auth.AdminLogado)
            return new UnauthorizedObjectResult(new { message = "Admin precisa estar logado." });

        return null;
    }
}
