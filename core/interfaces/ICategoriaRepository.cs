using TechStore.Core.Entities;

namespace TechStore.Core.Interfaces;

public interface ICategoriaRepository
{
    List<Categoria> GetAll();
    Categoria? GetById(int id);
    Categoria Add(Categoria categoria);
}
