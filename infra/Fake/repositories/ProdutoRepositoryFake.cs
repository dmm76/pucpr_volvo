using TechStore.Core.Entities;
using TechStore.Core.Interfaces;

namespace TechStore.Infra.Fake.Repositories;

public class ProdutoRepositoryFake : IProdutoRepository
{
    private readonly List<Produto> _data = new();
    private int _nextId = 0;

    public Produto? BuscarPorId(int id) => _data.FirstOrDefault(x => x.Id == id);

    public List<Produto> BuscarTodos() => _data.ToList();

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

    public bool ExisteNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return false;
        var n = nome.Trim();
        return _data.Any(x => x.Nome.Equals(n, StringComparison.OrdinalIgnoreCase));
    }

    public List<Produto> BuscarPorCategoria(int categoriaId) =>
        _data.Where(x => x.CategoriaId == categoriaId).ToList();
}
