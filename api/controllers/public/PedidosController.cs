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
    private const string VisitorHeaderName = "X-Visitor-Id";

    private readonly AuthState _auth;
    private readonly PedidoUseCases _useCases;
    private readonly CheckoutUseCases _checkout;

    public PedidosController(AuthState auth, PedidoUseCases useCases, CheckoutUseCases checkout)
    {
        _auth = auth;
        _useCases = useCases;
        _checkout = checkout;
    }

    private Guid? GetVisitorIdFromHeader()
    {
        Request.Headers.TryGetValue(VisitorHeaderName, out var header);
        return Guid.TryParse(header, out var parsed) ? parsed : null;
    }

    private IActionResult? BloquearSeNaoPodeAcessarPedido(PedidoDetalheDto dto)
    {
        var visitorId = GetVisitorIdFromHeader();

        // ALTERADO: usa PedidoGuard centralizado em vez de OwnershipGuard
        return PedidoGuard.BloquearSeNaoPodeAcessar(_auth, dto.ClienteId, dto.VisitorId, visitorId);
    }

    [HttpPost]
    public ActionResult<PedidoDetalheDto> CriarCarrinho()
    {
        var visitorId = GetVisitorIdFromHeader();

        var dto = _useCases.CriarCarrinho(visitorId);

        // devolve o "ticket" do carrinho para o cliente reutilizar nas próximas chamadas
        Response.Headers[VisitorHeaderName] = dto.VisitorId!.ToString();

        return Ok(dto);
    }

    [HttpGet("{pedidoId:int}")]
    public IActionResult BuscarPorId(int pedidoId)
    {
        var dto = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dto);
        if (block is not null)
            return block;

        return Ok(dto);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public IActionResult BuscarPorCliente(int clienteId)
    {
        // ALTERADO: login e ownership via PedidoGuard
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        // ALTERADO: validação de cliente via PedidoGuard
        var blockOwner = PedidoGuard.BloquearSeNaoPodeAssumirCliente(_auth, clienteId);
        if (blockOwner is not null)
            return blockOwner;

        var lista = _useCases.BuscarPorCliente(clienteId);
        return Ok(lista);
    }

    [HttpPost("{pedidoId:int}/itens")]
    public IActionResult AdicionarItem(int pedidoId, [FromBody] AddItemRequest request)
    {
        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual);
        if (block is not null)
            return block;

        var dto = _useCases.AdicionarItem(pedidoId, request.ProdutoId, request.Quantidade);
        return Ok(dto);
    }

    [HttpDelete("{pedidoId:int}/itens/{produtoId:int}")]
    public IActionResult RemoverItem(int pedidoId, int produtoId)
    {
        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual);
        if (block is not null)
            return block;

        var dto = _useCases.RemoverItem(pedidoId, produtoId);
        return Ok(dto);
    }

    [HttpPut("{pedidoId:int}/endereco")]
    public IActionResult DefinirEndereco(int pedidoId, [FromBody] SetEnderecoRequest request)
    {
        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual);
        if (block is not null)
            return block;

        var dto = _useCases.DefinirEnderecoEntrega(pedidoId, request.Endereco);
        return Ok(dto);
    }

    [HttpPut("{pedidoId:int}/pagamento")]
    public IActionResult DefinirPagamento(int pedidoId, [FromBody] SetPagamentoRequest request)
    {
        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual);
        if (block is not null)
            return block;

        var dto = _useCases.DefinirFormaPagamento(pedidoId, request.FormaPagamento);
        return Ok(dto);
    }

    [HttpPut("{pedidoId:int}/cliente")]
    public IActionResult IdentificarCliente(
        int pedidoId,
        [FromBody] IdentificarClienteRequest request
    )
    {
        // ALTERADO: login via PedidoGuard
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        // ALTERADO: impede "assumir" outro cliente via PedidoGuard
        var blockCliente = PedidoGuard.BloquearSeNaoPodeAssumirCliente(_auth, request.ClienteId);
        if (blockCliente is not null)
            return blockCliente;

        //ainda valida acesso ao pedido antes de mexer
        var dtoAtual = _useCases.BuscarPorId(pedidoId);
        if (dtoAtual.ClienteId is not null)
        {
            var block = BloquearSeNaoPodeAcessarPedido(dtoAtual);
            if (block is not null)
                return block;
        }

        var dto = _checkout.IdentificarClienteAutoSnapshot(pedidoId, request.ClienteId);
        return Ok(dto);
    }

    [HttpPut("{pedidoId:int}/usar-endereco-padrao/{clienteId:int}")]
    public IActionResult UsarEnderecoPadrao(int pedidoId, int clienteId)
    {
        // ALTERADO: login via PedidoGuard
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        // ALTERADO: owner do cliente via PedidoGuard
        var blockOwner = PedidoGuard.BloquearSeNaoPodeAssumirCliente(_auth, clienteId);
        if (blockOwner is not null)
            return blockOwner;

        // e valida acesso ao pedido também
        var dtoAtual = _useCases.BuscarPorId(pedidoId);
        var blockPedido = BloquearSeNaoPodeAcessarPedido(dtoAtual);
        if (blockPedido is not null)
            return blockPedido;

        var dto = _checkout.UsarEnderecoPadraoEntrega(pedidoId, clienteId);
        return Ok(dto);
    }

    [HttpPost("{pedidoId:int}/confirmar")]
    public IActionResult Confirmar(int pedidoId)
    {
        // ALTERADO: login via PedidoGuard
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        // valida acesso ao pedido (admin ou dono)
        var dtoAtual = _useCases.BuscarPorId(pedidoId);
        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual);
        if (block is not null)
            return block;

        return Ok(_useCases.Confirmar(pedidoId));
    }

    [HttpPost("{pedidoId:int}/pagar")]
    public IActionResult Pagar(int pedidoId)
    {
        // ALTERADO: login via PedidoGuard
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        // valida acesso ao pedido (admin ou dono)
        var dtoAtual = _useCases.BuscarPorId(pedidoId);
        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual);
        if (block is not null)
            return block;

        return Ok(_useCases.Pagar(pedidoId));
    }
}
