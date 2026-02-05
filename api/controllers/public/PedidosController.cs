using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Dtos;
using TechStore.Core.Dtos;
using TechStore.Core.UseCases.Pedidos;

namespace TechStore.Api.Controllers.Public;

[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly PedidoUseCases _useCases;

    public PedidosController(PedidoUseCases useCases)
    {
        _useCases = useCases;
    }

    [HttpPost]
    public ActionResult<PedidoDetalheDto> CriarCarrinho() => Ok(_useCases.CriarCarrinho());

    [HttpGet("{pedidoId:int}")]
    public ActionResult<PedidoDetalheDto> BuscarPorId(int pedidoId) =>
        Ok(_useCases.BuscarPorId(pedidoId));

    [HttpGet("cliente/{clienteId:int}")]
    public ActionResult<IReadOnlyList<PedidoDetalheDto>> BuscarPorCliente(int clienteId) =>
        Ok(_useCases.BuscarPorCliente(clienteId));

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

    [HttpPut("{pedidoId:int}/pagamento")]
    public ActionResult<PedidoDetalheDto> DefinirPagamento(
        int pedidoId,
        [FromBody] SetPagamentoRequest request
    ) => Ok(_useCases.DefinirFormaPagamento(pedidoId, request.FormaPagamento));

    [HttpPut("{pedidoId:int}/cliente")]
    public ActionResult<PedidoDetalheDto> IdentificarCliente(
        int pedidoId,
        [FromBody] IdentificarClienteRequest request
    ) =>
        Ok(_useCases.IdentificarCliente(pedidoId, request.ClienteId, request.CustomerNameSnapshot));

    [HttpPost("{pedidoId:int}/confirmar")]
    public ActionResult<PedidoDetalheDto> Confirmar(int pedidoId) =>
        Ok(_useCases.Confirmar(pedidoId));

    [HttpPost("{pedidoId:int}/pagar")]
    public ActionResult<PedidoDetalheDto> Pagar(int pedidoId) => Ok(_useCases.Pagar(pedidoId));
}
