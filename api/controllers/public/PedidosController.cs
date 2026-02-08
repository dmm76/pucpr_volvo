using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos.Pedidos;
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

    // =========================================
    // Helpers
    // =========================================
    private static Guid? ParseGuidOrNull(string? value) =>
        Guid.TryParse(value, out var g) ? g : null;

    private Guid? GetVisitorIdFromHeader()
    {
        Request.Headers.TryGetValue(VisitorHeaderName, out var header);
        return ParseGuidOrNull(header);
    }

    // Versão "normal": usa header do request (continua funcionando fora do Swagger)
    private IActionResult? BloquearSeNaoPodeAcessarPedido(PedidoDetalheDto dto)
    {
        var visitorId = GetVisitorIdFromHeader();
        return PedidoGuard.BloquearSeNaoPodeAcessar(_auth, dto.ClienteId, dto.VisitorId, visitorId);
    }

    // Versão "Swagger-friendly": recebe visitorId explicitamente (aparece no Swagger)
    private IActionResult? BloquearSeNaoPodeAcessarPedido(PedidoDetalheDto dto, Guid? visitorId)
    {
        // se não veio por parâmetro, cai no header (não quebra chamadas antigas)
        visitorId ??= GetVisitorIdFromHeader();

        return PedidoGuard.BloquearSeNaoPodeAcessar(_auth, dto.ClienteId, dto.VisitorId, visitorId);
    }

    // =========================================
    // Endpoints
    // =========================================

    [HttpPost]
    public ActionResult<PedidoDetalheDto> CriarCarrinho(
        // só para o Swagger permitir que você "continue" um visitorId existente se quiser
        [FromHeader(Name = VisitorHeaderName)] Guid? visitorId = null
    )
    {
        // prioridade: parâmetro (Swagger) -> header real
        visitorId ??= GetVisitorIdFromHeader();

        var dto = _useCases.CriarCarrinho(visitorId);

        // devolve o "ticket" do carrinho para o cliente reutilizar nas próximas chamadas
        if (dto.VisitorId is not null)
            Response.Headers[VisitorHeaderName] = dto.VisitorId.Value.ToString();

        return Ok(dto);
    }

    [HttpGet("{pedidoId:int}")]
    public IActionResult BuscarPorId(
        int pedidoId,
        [FromHeader(Name = VisitorHeaderName)] Guid? visitorId = null
    )
    {
        var dto = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dto, visitorId);
        if (block is not null)
            return block;

        return Ok(dto);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public IActionResult BuscarPorCliente(int clienteId)
    {
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        var blockOwner = PedidoGuard.BloquearSeNaoPodeAssumirCliente(_auth, clienteId);
        if (blockOwner is not null)
            return blockOwner;

        var lista = _useCases.BuscarPorCliente(clienteId);
        return Ok(lista);
    }

    [HttpPost("{pedidoId:int}/itens")]
    public IActionResult AdicionarItem(
        int pedidoId,
        [FromBody] AddItemRequest request,
        [FromHeader(Name = VisitorHeaderName)] Guid? visitorId = null
    )
    {
        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual, visitorId);
        if (block is not null)
            return block;

        var dto = _useCases.AdicionarItem(pedidoId, request.ProdutoId, request.Quantidade);
        return Ok(dto);
    }

    [HttpDelete("{pedidoId:int}/itens/{produtoId:int}")]
    public IActionResult RemoverItem(
        int pedidoId,
        int produtoId,
        [FromHeader(Name = VisitorHeaderName)] Guid? visitorId = null
    )
    {
        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual, visitorId);
        if (block is not null)
            return block;

        var dto = _useCases.RemoverItem(pedidoId, produtoId);
        return Ok(dto);
    }

    [HttpPut("{pedidoId:int}/endereco")]
    public IActionResult DefinirEndereco(
        int pedidoId,
        [FromBody] SetEnderecoRequest request,
        [FromHeader(Name = VisitorHeaderName)] Guid? visitorId = null
    )
    {
        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual, visitorId);
        if (block is not null)
            return block;

        var dto = _useCases.DefinirEnderecoEntrega(pedidoId, request.Endereco);
        return Ok(dto);
    }

    [HttpPut("{pedidoId:int}/pagamento")]
    public IActionResult DefinirPagamento(
        int pedidoId,
        [FromBody] SetPagamentoRequest request,
        [FromHeader(Name = VisitorHeaderName)] Guid? visitorId = null
    )
    {
        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual, visitorId);
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
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        var blockCliente = PedidoGuard.BloquearSeNaoPodeAssumirCliente(_auth, request.ClienteId);
        if (blockCliente is not null)
            return blockCliente;

        // valida acesso ao pedido antes de mexer
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
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        var blockOwner = PedidoGuard.BloquearSeNaoPodeAssumirCliente(_auth, clienteId);
        if (blockOwner is not null)
            return blockOwner;

        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var blockPedido = BloquearSeNaoPodeAcessarPedido(dtoAtual);
        if (blockPedido is not null)
            return blockPedido;

        var dto = _checkout.UsarEnderecoPadraoEntrega(pedidoId, clienteId);
        return Ok(dto);
    }

    [HttpPost("{pedidoId:int}/confirmar")]
    public IActionResult Confirmar(
        int pedidoId,
        [FromHeader(Name = VisitorHeaderName)] Guid? visitorId = null
    )
    {
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual, visitorId);
        if (block is not null)
            return block;

        return Ok(_useCases.Confirmar(pedidoId));
    }

    [HttpPost("{pedidoId:int}/pagar")]
    public IActionResult Pagar(
        int pedidoId,
        [FromHeader(Name = VisitorHeaderName)] Guid? visitorId = null
    )
    {
        var blockLogin = PedidoGuard.BloquearCheckoutSeNaoLogado(_auth);
        if (blockLogin is not null)
            return blockLogin;

        var dtoAtual = _useCases.BuscarPorId(pedidoId);

        var block = BloquearSeNaoPodeAcessarPedido(dtoAtual, visitorId);
        if (block is not null)
            return block;

        return Ok(_useCases.Pagar(pedidoId));
    }
}
