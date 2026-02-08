using TechStore.Core.Dtos.Relatorios;

namespace TechStore.Core.Interfaces;

public interface IRelatorioRepository
{
    IReadOnlyList<VendasPorCategoriaDto> VendasPorCategoria();
}
