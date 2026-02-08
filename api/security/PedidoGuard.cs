using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Core.Entities;

namespace TechStore.Api.Security;

public static class PedidoGuard
{
    public static bool PodeAcessar(Pedido pedido, AuthState auth, Guid? visitorId)
    {
        // Admin pode tudo
        if (auth.UserRole == UserRole.Admin)
            return true;

        // Pedido visitante → posse
        if (pedido.VisitorId is not null)
            return pedido.VisitorId == visitorId;

        // Pedido tem cliente → ownership
        if (pedido.ClienteId is not null)
            return pedido.ClienteId == auth.ClienteId;

        return false;
    }

    public static IActionResult? BloquearSeNaoPodeAcessar(
        AuthState auth,
        int? resourceClienteId,
        Guid? resourceVisitorId,
        Guid? requestVisitorId
    )
    {
        if (auth.UserRole == UserRole.Admin)
            return null;

        if (resourceVisitorId is not null && resourceVisitorId == requestVisitorId)
            return null;

        if (auth.UserLogado)
        {
            if (auth.ClienteId == resourceClienteId)
                return null;

            return new ObjectResult(new { message = "Acesso negado (nao e o dono do recurso)." })
            {
                StatusCode = 403,
            };
        }

        return new ObjectResult(new { message = "Acesso negado." }) { StatusCode = 403 };
    }

    public static IActionResult? BloquearCheckoutSeNaoLogado(AuthState auth)
    {
        if (!auth.UserLogado)
            return new UnauthorizedObjectResult(new { message = "Usuario precisa estar logado." });

        return null;
    }

    public static IActionResult? BloquearSeNaoPodeAssumirCliente(AuthState auth, int? clienteId)
    {
        if (auth.UserRole == UserRole.Admin)
            return null;

        if (auth.ClienteId is null)
            return new ObjectResult(new { message = "Usuario nao possui cliente associado." })
            {
                StatusCode = 403,
            };

        if (auth.ClienteId != clienteId)
            return new ObjectResult(
                new { message = "Acesso negado (clienteId nao pertence ao usuario logado)." }
            )
            {
                StatusCode = 403,
            };

        return null;
    }
}
