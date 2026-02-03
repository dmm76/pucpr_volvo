using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.Exceptions;
using TechStore.Core.Interfaces;

namespace TechStore.Core.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _repo;

    public CategoriaService(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    public List<CategoriaDto> Listar() =>
        _repo.GetAll().Select(c => new CategoriaDto(c.Id, c.Nome)).ToList();

    public CategoriaDto BuscarPorId(int id)
    {
        var c = _repo.GetById(id);
        if (c is null)
            throw new NotFoundException("Categoria não encontrada.");

        return new CategoriaDto(c.Id, c.Nome);
    }

    public CategoriaDto Criar(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BusinessRuleException("Nome da categoria é obrigatório.");

        var criada = _repo.Add(new Categoria { Nome = nome.Trim() });
        return new CategoriaDto(criada.Id, criada.Nome);
    }
}
