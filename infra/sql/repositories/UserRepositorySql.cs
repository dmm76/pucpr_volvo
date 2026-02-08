using Microsoft.EntityFrameworkCore;
using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Context;

namespace TechStore.Infra.Sql.Repositories;

public class UserRepositorySql : IUserRepository
{
    private readonly TechStoreDbContext _ctx;

    public UserRepositorySql(TechStoreDbContext ctx)
    {
        _ctx = ctx;
    }

    public User? BuscarPorId(int id) => _ctx.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);

    public User? BuscarPorLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;

        var l = login.Trim().ToLowerInvariant();
        return _ctx.Users.AsNoTracking().FirstOrDefault(x => x.Login == l);
    }

    public List<User> BuscarTodos() => _ctx.Users.AsNoTracking().ToList();

    public User Inserir(User user)
    {
        _ctx.Users.Add(user);
        _ctx.SaveChanges();
        return user;
    }

    public void Atualizar(User user)
    {
        _ctx.Users.Update(user);
        _ctx.SaveChanges();
    }

    public void Remover(int id)
    {
        var entity = _ctx.Users.FirstOrDefault(x => x.Id == id);
        if (entity is null)
            return;

        _ctx.Users.Remove(entity);
        _ctx.SaveChanges();
    }

    public bool ExisteLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return false;
        var l = login.Trim().ToLowerInvariant();
        return _ctx.Users.Any(x => x.Login == l);
    }

    public bool ExisteEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        var e = email.Trim().ToLowerInvariant();
        return _ctx.Users.Any(x => x.Email == e);
    }
}
