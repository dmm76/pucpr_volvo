using TechStore.Core.Entities;

namespace TechStore.Core.Interfaces;

public interface ICategoriaRepository
{
    Categoria? BuscarPorId(int id);

    List<Categoria> BuscarTodos();

    Categoria Inserir(Categoria categoria);
    void Atualizar(Categoria categoria);
    void Remover(int id);

    bool ExisteNome(string nome);

    bool ExistePorId(int id);
}
