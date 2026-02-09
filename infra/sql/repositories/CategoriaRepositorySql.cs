using Microsoft.EntityFrameworkCore;
using TechStore.Core.Entities;
using TechStore.Core.Interfaces;
using TechStore.Infra.Context;

namespace TechStore.Infra.Sql.Repositories;

public class CategoriaRepositorySql : ICategoriaRepository
{
    private readonly TechStoreDbContext _ctx;

    public CategoriaRepositorySql(TechStoreDbContext ctx) => _ctx = ctx;

    public Categoria? BuscarPorId(int id) =>
        _ctx.Categorias.AsNoTracking().FirstOrDefault(x => x.Id == id);

    public List<Categoria> BuscarTodos() => _ctx.Categorias.AsNoTracking().ToList();

    public Categoria Inserir(Categoria categoria)
    {
        _ctx.Categorias.Add(categoria);
        _ctx.SaveChanges();
        return categoria;
    }

    public void Atualizar(Categoria categoria)
    {
        _ctx.Categorias.Update(categoria);
        _ctx.SaveChanges();
    }

    public void Remover(int id)
    {
        var entity = _ctx.Categorias.FirstOrDefault(x => x.Id == id);
        if (entity is null)
            return;
        _ctx.Categorias.Remove(entity);
        _ctx.SaveChanges();
    }

    public bool ExisteNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return false;
        var n = nome.Trim();
        return _ctx.Categorias.Any(x => x.Nome == n);
    }

    public bool ExistePorId(int id)
    {
        if (id <= 0)
            return false;

        var exists = _ctx.Categorias.Any(c => c.Id == id);
        return exists;
    }
}
