using TechStore.Core.Entities;

namespace TechStore.Core.Interfaces;

public interface IProdutoRepository
{
    Produto? BuscarPorId(int id);
    IReadOnlyList<Produto> BuscarTodos();

    Produto Inserir(Produto produto);
    void Atualizar(Produto produto);
    void Remover(int id);

    bool NomeJaExiste(string nome);
    IReadOnlyList<Produto> BuscarPorCategoria(int categoriaId);
}
