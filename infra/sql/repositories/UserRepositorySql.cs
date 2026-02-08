using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Fake;

namespace TechStore.Infra.Sql.Repositories;

public class UserRepositorySql : IUserRepository
{
    private readonly List<User> _data = new();
    private int _nextId = 0;

    public UserRepositorySql(IPasswordHasher hasher)
    {
        var admin = new User(
            login: "admin",
            email: "admin@techstore.com",
            senhaHash: hasher.Hash("Admin@123"),
            role: UserRole.Admin
        );

        var id = Interlocked.Increment(ref _nextId);
        FakeEntitySetter.SetPrivateId(admin, id);

        _data.Add(admin);
    }

    public User? BuscarPorId(int id) => _data.FirstOrDefault(x => x.Id == id);

    public User? BuscarPorLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;
        var l = login.Trim().ToLowerInvariant();
        return _data.FirstOrDefault(x => x.Login == l);
    }

    public List<User> BuscarTodos() => _data.ToList();

    public User Inserir(User user)
    {
        var id = Interlocked.Increment(ref _nextId);
        FakeEntitySetter.SetPrivateId(user, id);

        _data.Add(user);
        return user;
    }

    public void Atualizar(User user)
    {
        var idx = _data.FindIndex(x => x.Id == user.Id);
        if (idx < 0)
            return;

        _data[idx] = user;
    }

    public void Remover(int id)
    {
        var u = BuscarPorId(id);
        if (u is null)
            return;

        _data.Remove(u);
    }

    public bool ExisteLogin(string login) =>
        !string.IsNullOrWhiteSpace(login)
        && _data.Any(x => x.Login == login.Trim().ToLowerInvariant());

    public bool ExisteEmail(string email) =>
        !string.IsNullOrWhiteSpace(email)
        && _data.Any(x => x.Email == email.Trim().ToLowerInvariant());
}
