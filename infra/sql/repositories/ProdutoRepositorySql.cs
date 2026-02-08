using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Fake;
using TechStore.Infra.Fake.Factories;

namespace TechStore.Infra.Sql.Repositories;

public class ProdutoRepositorySql : IProdutoRepository
{
    private readonly List<Produto> _data = new();
    private int _nextId = 0;

    public ProdutoRepositorySql()
    {
        _data = ProdutoFactory.Criar();

        // atribui ids 1..N no seed
        for (int i = 0; i < _data.Count; i++)
        {
            var id = i + 1;
            FakeEntitySetter.SetPrivateId(_data[i], id);
            _nextId = id;
        }
    }

    public Produto? BuscarPorId(int id) => _data.FirstOrDefault(x => x.Id == id);

    public IReadOnlyList<Produto> BuscarTodos() => _data;

    public Produto Inserir(Produto produto)
    {
        var id = Interlocked.Increment(ref _nextId);
        FakeEntitySetter.SetPrivateId(produto, id);

        _data.Add(produto);
        return produto;
    }

    public void Atualizar(Produto produto)
    {
        var idx = _data.FindIndex(x => x.Id == produto.Id);
        if (idx < 0)
            return;

        _data[idx] = produto;
    }

    public void Remover(int id)
    {
        var p = BuscarPorId(id);
        if (p is null)
            return;

        _data.Remove(p);
    }

    public bool NomeJaExiste(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return false;
        var n = nome.Trim();
        return _data.Any(x => x.Nome.Trim().Equals(n, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyList<Produto> BuscarPorCategoria(int categoriaId) =>
        _data.Where(x => x.CategoriaId == categoriaId).ToList();
}
