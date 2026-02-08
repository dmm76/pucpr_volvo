using Microsoft.EntityFrameworkCore;
using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Context;

namespace TechStore.Infra.Sql.Repositories;

public class ClienteRepositorySql : IClienteRepository
{
    private readonly TechStoreDbContext _ctx;

    public ClienteRepositorySql(TechStoreDbContext ctx) => _ctx = ctx;

    public Cliente? BuscarPorId(int id) =>
        _ctx.Clientes.Include(c => c.Enderecos).FirstOrDefault(c => c.Id == id);

    public Cliente? BuscarPorUserId(int userId) =>
        _ctx.Clientes.Include(c => c.Enderecos).FirstOrDefault(c => c.UserId == userId);

    public List<Cliente> BuscarTodos() => _ctx.Clientes.AsNoTracking().ToList();

    public Cliente Inserir(Cliente cliente)
    {
        _ctx.Clientes.Add(cliente);
        _ctx.SaveChanges();
        return cliente;
    }

    public void Atualizar(Cliente cliente)
    {
        _ctx.Clientes.Update(cliente);
        _ctx.SaveChanges();
    }

    public void Remover(int id)
    {
        var entity = _ctx.Clientes.FirstOrDefault(x => x.Id == id);
        if (entity is null)
            return;
        _ctx.Clientes.Remove(entity);
        _ctx.SaveChanges();
    }

    public bool ExistePorUserId(int userId) => _ctx.Clientes.Any(x => x.UserId == userId);
}
