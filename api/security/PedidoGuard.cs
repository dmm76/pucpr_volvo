using TechStore.Api.Auth;
using TechStore.Core.Entities;

public static class PedidoGuard
{
    public static bool PodeAcessar(Pedido pedido, AuthState auth, Guid? visitorId)
    {
        // Admin pode tudo
        if (auth.UserRole == UserRole.Admin)
            return true;

        // Pedido tem cliente → ownership
        if (pedido.ClienteId is not null)
            return pedido.ClienteId == auth.ClienteId;

        // Pedido visitante → posse
        if (pedido.VisitorId is not null)
            return pedido.VisitorId == visitorId;

        return false;
    }
}
