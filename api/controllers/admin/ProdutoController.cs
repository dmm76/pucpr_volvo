using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos.Produtos;
using TechStore.Api.Security;
using TechStore.Core.Dtos;
using TechStore.Core.UseCases.Produtos;

namespace TechStore.Api.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoUseCases _useCases;
    private readonly AuthState _auth;

    public ProdutoController(ProdutoUseCases useCases, AuthState auth)
    {
        _useCases = useCases;
        _auth = auth;
    }

    [HttpGet("{id:int}")]
    public ActionResult<ProdutoDto> BuscarPorId(int id) => Ok(_useCases.BuscarPorId(id));

    [HttpGet]
    public IActionResult BuscarTodos([FromQuery] BuscarProdutosQuery query)
    {
        var skip = query.Skip < 0 ? 0 : query.Skip;

        var take = query.Take <= 0 ? 10 : query.Take;
        if (take > 100)
            take = 100;

        if (
            query.PrecoMin.HasValue
            && query.PrecoMax.HasValue
            && query.PrecoMin.Value > query.PrecoMax.Value
        )
            return BadRequest("precoMin não pode ser maior que precoMax.");

        var itens = _useCases.BuscarComFiltros(
            query.Nome,
            query.PrecoMin,
            query.PrecoMax,
            skip,
            take
        );

        return Ok(itens);
    }

    [HttpGet("categoria/{categoriaId:int}")]
    public ActionResult<IReadOnlyList<ProdutoDto>> BuscarPorCategoria(int categoriaId) =>
        Ok(_useCases.BuscarPorCategoria(categoriaId));

    [HttpPost]
    public IActionResult Criar([FromBody] CriarProdutoRequest request)
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        var produto = _useCases.Criar(
            request.CategoriaId,
            request.Nome,
            request.Descricao,
            request.Preco,
            request.Estoque
        );

        return CreatedAtAction(nameof(BuscarPorId), new { id = produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    public IActionResult Atualizar(int id, [FromBody] AtualizarProdutoRequest request)
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        var produto = _useCases.Atualizar(
            id,
            request.Nome,
            request.Descricao,
            request.Preco,
            request.Estoque,
            request.CategoriaId
        );

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Remover(int id)
    {
        var bloqueio = AdminGuard.BloquearSeNaoLogado(_auth);
        if (bloqueio is not null)
            return bloqueio;

        _useCases.Remover(id);
        return NoContent();
    }
}
