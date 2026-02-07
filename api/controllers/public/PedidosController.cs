using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos;
using TechStore.Api.Security;
using TechStore.Core.Dtos;
using TechStore.Core.UseCases.Pedidos;

namespace TechStore.Api.Controllers.Public;

[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly AuthState _auth;
    private readonly PedidoUseCases _useCases;
    private readonly CheckoutUseCases _checkout;

    public PedidosController(AuthState auth, PedidoUseCases useCases, CheckoutUseCases checkout)
    {
        _auth = auth;
        _useCases = useCases;
        _checkout = checkout;
    }

    [HttpPost]
    public ActionResult<PedidoDetalheDto> CriarCarrinho() => Ok(_useCases.CriarCarrinho());

    [HttpGet("{pedidoId:int}")]
    public IActionResult BuscarPorId(int pedidoId)
    {
        var clienteIdDoPedido = _useCases.BuscarClienteIdDoPedido(pedidoId);

        if (clienteIdDoPedido is not null) // só trava quando já tem dono
        {
            var block = OwnershipGuard.BloquearSeNaoDonoOuAdmin(_auth, clienteIdDoPedido);
            if (block is not null)
                return block;
        }

        var dto = _useCases.BuscarPorId(pedidoId);
        return Ok(dto);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public IActionResult BuscarPorCliente(int clienteId)
    {
        var block = OwnershipGuard.BloquearSeNaoDonoOuAdmin(_auth, clienteId);
        if (block is not null)
            return block;

        var lista = _useCases.BuscarPorCliente(clienteId);
        return Ok(lista);
    }

    [HttpPost("{pedidoId:int}/itens")]
    public ActionResult<PedidoDetalheDto> AdicionarItem(
        int pedidoId,
        [FromBody] AddItemRequest request
    ) => Ok(_useCases.AdicionarItem(pedidoId, request.ProdutoId, request.Quantidade));

    [HttpDelete("{pedidoId:int}/itens/{produtoId:int}")]
    public ActionResult<PedidoDetalheDto> RemoverItem(int pedidoId, int produtoId) =>
        Ok(_useCases.RemoverItem(pedidoId, produtoId));

    [HttpPut("{pedidoId:int}/endereco")]
    public ActionResult<PedidoDetalheDto> DefinirEndereco(
        int pedidoId,
        [FromBody] SetEnderecoRequest request
    ) => Ok(_useCases.DefinirEnderecoEntrega(pedidoId, request.Endereco));

    [HttpPut("{pedidoId:int}/usar-endereco-padrao/{clienteId:int}")]
    public ActionResult<PedidoDetalheDto> UsarEnderecoPadrao(int pedidoId, int clienteId) =>
        Ok(_checkout.UsarEnderecoPadraoEntrega(pedidoId, clienteId));

    [HttpPut("{pedidoId:int}/pagamento")]
    public ActionResult<PedidoDetalheDto> DefinirPagamento(
        int pedidoId,
        [FromBody] SetPagamentoRequest request
    ) => Ok(_useCases.DefinirFormaPagamento(pedidoId, request.FormaPagamento));

    [HttpPut("{pedidoId:int}/cliente")]
    public ActionResult<PedidoDetalheDto> IdentificarCliente(
        int pedidoId,
        [FromBody] IdentificarClienteRequest request
    ) => Ok(_checkout.IdentificarClienteAutoSnapshot(pedidoId, request.ClienteId));

    [HttpPost("{pedidoId:int}/confirmar")]
    public ActionResult<PedidoDetalheDto> Confirmar(int pedidoId) =>
        Ok(_useCases.Confirmar(pedidoId));

    [HttpPost("{pedidoId:int}/pagar")]
    public ActionResult<PedidoDetalheDto> Pagar(int pedidoId) => Ok(_useCases.Pagar(pedidoId));
}
