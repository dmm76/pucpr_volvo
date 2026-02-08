using Microsoft.EntityFrameworkCore;
using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Context;

namespace TechStore.Infra.Sql.Repositories;

public class ProdutoRepositorySql : IProdutoRepository
{
    private readonly TechStoreDbContext _ctx;

    public ProdutoRepositorySql(TechStoreDbContext ctx) => _ctx = ctx;

    public Produto? BuscarPorId(int id) =>
        _ctx.Produtos.AsNoTracking().FirstOrDefault(x => x.Id == id);

    public IReadOnlyList<Produto> BuscarTodos() => _ctx.Produtos.AsNoTracking().ToList();

    public Produto Inserir(Produto produto)
    {
        _ctx.Produtos.Add(produto);
        _ctx.SaveChanges();
        return produto;
    }

    public void Atualizar(Produto produto)
    {
        _ctx.Produtos.Update(produto);
        _ctx.SaveChanges();
    }

    public void Remover(int id)
    {
        var entity = _ctx.Produtos.FirstOrDefault(x => x.Id == id);
        if (entity is null)
            return;

        _ctx.Produtos.Remove(entity);
        _ctx.SaveChanges();
    }

    public bool NomeJaExiste(string nome)
    {
        var n = (nome ?? "").Trim();
        if (n.Length == 0)
            return false;

        return _ctx.Produtos.Any(x => x.Nome == n);
    }

    public IReadOnlyList<Produto> BuscarPorCategoria(int categoriaId) =>
        _ctx.Produtos.AsNoTracking().Where(x => x.CategoriaId == categoriaId).ToList();

    public List<ProdutoDto> BuscarTodosPaginado(int skip, int take) =>
        _ctx
            .Produtos.AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip(skip)
            .Take(take)
            .Select(p => new ProdutoDto(
                p.Id,
                p.Nome ?? "",
                p.Descricao,
                p.PrecoAtual,
                p.Estoque,
                p.CategoriaId
            ))
            .ToList();
}
