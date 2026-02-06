using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Core.Entities;

namespace TechStore.Api.Security;

public static class OwnershipGuard
{
    public static IActionResult? BloquearSeNaoDonoOuAdmin(AuthState auth, int? resourceClienteId)
    {
        if (!auth.UserLogado)
            return new UnauthorizedObjectResult(new { message = "Usuario precisa estar logado." });

        if (auth.UserRole == UserRole.Admin)
            return null;

        if (auth.ClienteId is null)
            return new ObjectResult(new { message = "Usuario nao possui cliente associado." })
            {
                StatusCode = 403,
            };

        if (resourceClienteId is null)
            return new ObjectResult(new { message = "Recurso nao possui dono associado." })
            {
                StatusCode = 403,
            };

        if (auth.ClienteId != resourceClienteId)
            return new ObjectResult(new { message = "Acesso negado (nao e o dono do recurso)." })
            {
                StatusCode = 403,
            };

        return null;
    }
}
