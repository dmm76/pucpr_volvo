using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Fake.Factories;

namespace TechStore.Infra.Fake.Repositories;

public class CategoriaRepositoryFake : ICategoriaRepository
{
    private readonly List<Categoria> _categorias;

    public CategoriaRepositoryFake()
    {
        _categorias = CategoriaFactory.Criar();
    }

    public List<Categoria> GetAll() => _categorias;

    public Categoria? GetById(int id) => _categorias.FirstOrDefault(c => c.Id == id);

    public Categoria Add(Categoria categoria)
    {
        var novoId = _categorias.Count == 0 ? 1 : _categorias.Max(c => c.Id) + 1;
        categoria.Id = novoId;
        _categorias.Add(categoria);
        return categoria;
    }
}
