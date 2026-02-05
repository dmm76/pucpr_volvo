using TechStore.Core.Entities;

namespace TechStore.Core.Interfaces;

public interface IUserRepository
{
    User? BuscarPorId(int id);
    User? BuscarPorLogin(string login);
    List<User> BuscarTodos();

    User Inserir(User user);
    void Atualizar(User user);
    void Remover(int id);

    bool ExisteLogin(string login);
    bool ExisteEmail(string email);
}
