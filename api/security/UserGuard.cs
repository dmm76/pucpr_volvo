using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;

namespace TechStore.Api.Security;

public static class UserGuard
{
    public static IActionResult? BloquearSeNaoLogado(AuthState auth)
    {
        if (!auth.UserLogado)
            return new UnauthorizedObjectResult(new { message = "Usuario precisa estar logado." });

        return null;
    }
}
