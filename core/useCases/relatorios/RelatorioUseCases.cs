using TechStore.Core.Dtos.Relatorios;
using TechStore.Core.Interfaces;

namespace TechStore.Core.UseCases.Relatorios;

public class RelatorioUseCases
{
    private readonly IRelatorioRepository _repo;

    public RelatorioUseCases(IRelatorioRepository repo) => _repo = repo;

    public IReadOnlyList<VendasPorCategoriaDto> VendasPorCategoria() => _repo.VendasPorCategoria();
}
