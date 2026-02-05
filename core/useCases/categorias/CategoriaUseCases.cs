using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.Exceptions;
using TechStore.Core.Interfaces;

namespace TechStore.Core.useCases.categorias;

public class CategoriaUseCases
{
    private readonly ICategoriaRepository _repo;

    public CategoriaUseCases(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    public List<CategoriaDto> Listar() =>
        _repo.BuscarTodos().Select(c => new CategoriaDto(c.Id, c.Nome)).ToList();

    public CategoriaDto BuscarPorId(int id)
    {
        var c = _repo.BuscarPorId(id);
        if (c is null)
            throw new NotFoundException("Categoria não encontrada.");

        return new CategoriaDto(c.Id, c.Nome);
    }

    public CategoriaDto Criar(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BusinessRuleException("Nome da categoria é obrigatório.");

        if (_repo.ExisteNome(nome))
            throw new BusinessRuleException("Já existe uma categoria com esse nome.");

        var criada = _repo.Inserir(new Categoria(nome));
        return new CategoriaDto(criada.Id, criada.Nome);
    }
}
