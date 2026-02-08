using Microsoft.EntityFrameworkCore;
using TechStore.Core.Dtos.Relatorios;
using TechStore.Core.Interfaces;
using TechStore.Infra.Context;

namespace TechStore.Infra.Sql.Repositories;

public class RelatorioRepositorySql : IRelatorioRepository
{
    private readonly TechStoreDbContext _ctx;

    public RelatorioRepositorySql(TechStoreDbContext ctx) => _ctx = ctx;

    public IReadOnlyList<VendasPorCategoriaDto> VendasPorCategoria()
    {
        // 1) Calcula os totais por CategoriaId (SQL traduz bem)
        var totais = (
            from i in _ctx.ItensPedido.AsNoTracking()
            join p in _ctx.Produtos.AsNoTracking() on i.ProdutoId equals p.Id
            group new { i, p } by p.CategoriaId into g
            select new
            {
                CategoriaId = g.Key,
                Total = g.Sum(x => x.i.PrecoUnitarioSnapshot * (decimal)x.i.Quantidade),
            }
        ).ToList();

        // 2) Busca nomes das categorias (SQL simples)
        var ids = totais.Select(t => t.CategoriaId).Distinct().ToList();

        var nomes = _ctx
            .Categorias.AsNoTracking()
            .Where(c => ids.Contains(c.Id))
            .Select(c => new { c.Id, c.Nome })
            .ToDictionary(x => x.Id, x => x.Nome);

        // 3) Monta DTO em memória
        return totais
            .Select(t => new VendasPorCategoriaDto(
                t.CategoriaId,
                nomes.TryGetValue(t.CategoriaId, out var nome) ? nome : "Sem categoria",
                t.Total
            ))
            .OrderByDescending(x => x.TotalVendido)
            .ToList();
    }
}
