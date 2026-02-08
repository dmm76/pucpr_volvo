using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.Exceptions;
using TechStore.Core.Interfaces;

namespace TechStore.Core.UseCases.Produtos;

public class ProdutoUseCases
{
    private readonly IProdutoRepository _repo;

    public ProdutoUseCases(IProdutoRepository repo)
    {
        _repo = repo;
    }

    public ProdutoDto Criar(
        int categoriaId,
        string nome,
        string? descricao,
        decimal preco,
        int estoque
    )
    {
        if (_repo.NomeJaExiste(nome))
            throw new BusinessRuleException(ErrorCodes.ProductNameAlreadyExists);

        var produto = new Produto(categoriaId, nome, preco, estoque, descricao);

        _repo.Inserir(produto);

        return Map(produto);
    }

    public ProdutoDto Atualizar(
        int id,
        string nome,
        string? descricao,
        decimal preco,
        int estoque,
        int categoriaId
    )
    {
        var produto =
            _repo.BuscarPorId(id) ?? throw new NotFoundException(ErrorCodes.ProductNotFound);

        // evita conflito de nome
        if (
            !produto.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)
            && _repo.NomeJaExiste(nome)
        )
        {
            throw new BusinessRuleException(ErrorCodes.ProductNameAlreadyExists);
        }

        produto.DefinirCategoria(categoriaId);
        produto.AtualizarNome(nome);
        produto.AtualizarDescricao(descricao);
        produto.AtualizarPreco(preco);
        produto.AjustarEstoque(estoque);

        _repo.Atualizar(produto);

        return Map(produto);
    }

    public void Remover(int id)
    {
        var produto =
            _repo.BuscarPorId(id) ?? throw new NotFoundException(ErrorCodes.ProductNotFound);

        _repo.Remover(produto.Id);
    }

    public ProdutoDto BuscarPorId(int id)
    {
        var produto =
            _repo.BuscarPorId(id) ?? throw new NotFoundException(ErrorCodes.ProductNotFound);

        return Map(produto);
    }

    public IReadOnlyList<ProdutoDto> BuscarTodos() => _repo.BuscarTodos().Select(Map).ToList();

    public IReadOnlyList<ProdutoDto> BuscarPorCategoria(int categoriaId) =>
        _repo.BuscarPorCategoria(categoriaId).Select(Map).ToList();

    private static ProdutoDto Map(Produto p) =>
        new(p.Id, p.Nome, p.Descricao, p.PrecoAtual, p.Estoque, p.CategoriaId);

    public IReadOnlyList<ProdutoDto> BuscarTodos(int skip, int take) =>
        _repo.BuscarTodosPaginado(skip, take);

    public IReadOnlyList<ProdutoDto> BuscarComFiltros(
        string? nome,
        decimal? precoMin,
        decimal? precoMax,
        int skip,
        int take
    ) => _repo.BuscarComFiltros(nome, precoMin, precoMax, skip, take);
}
