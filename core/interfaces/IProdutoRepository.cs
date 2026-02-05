using TechStore.Core.Entities;

namespace TechStore.Core.Interfaces;

public interface IProdutoRepository
{
    Produto? BuscarPorId(int id);
    List<Produto> BuscarTodos();

    Produto Inserir(Produto produto);
    void Atualizar(Produto produto);
    void Remover(int id);

    bool ExisteNome(string nome);
    List<Produto> BuscarPorCategoria(int categoriaId);
}
