using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Fake.Factories;

namespace TechStore.Infra.Fake.Repositories;

public class CategoriaRepositoryFake : ICategoriaRepository
{
    private readonly List<Categoria> _data;
    private int _nextId = 0;

    public CategoriaRepositoryFake()
    {
        _data = CategoriaFactory.Criar();

        // atribui ids 1..N no seed
        for (int i = 0; i < _data.Count; i++)
        {
            var id = i + 1;
            FakeEntitySetter.SetPrivateId(_data[i], id);
            _nextId = id;
        }
    }

    public Categoria? BuscarPorId(int id) => _data.FirstOrDefault(x => x.Id == id);

    public List<Categoria> BuscarTodos() => _data.ToList();

    public Categoria Inserir(Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        var id = Interlocked.Increment(ref _nextId);
        FakeEntitySetter.SetPrivateId(categoria, id);

        _data.Add(categoria);
        return categoria;
    }

    public void Atualizar(Categoria categoria)
    {
        if (categoria is null)
            return;

        var idx = _data.FindIndex(x => x.Id == categoria.Id);
        if (idx < 0)
            return;

        _data[idx] = categoria;
    }

    public void Remover(int id)
    {
        var c = BuscarPorId(id);
        if (c is null)
            return;

        _data.Remove(c);
    }

    public bool ExisteNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return false;

        var n = nome.Trim();
        return _data.Any(x => x.Nome.Equals(n, StringComparison.OrdinalIgnoreCase));
    }
}
