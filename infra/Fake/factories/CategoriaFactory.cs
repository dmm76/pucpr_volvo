using TechStore.Core.Entities;

namespace TechStore.Infra.Fake.Factories;

public static class CategoriaFactory
{
    public static List<Categoria> Criar() =>
        new()
        {
            new Categoria
            {
                Id = 1,
                Nome = "Periféricos",
                Descricao = "Teclado, Mouse e etc",
            },
            new Categoria
            {
                Id = 2,
                Nome = "Hardware",
                Descricao = "Equipementos esseciais",
            },
            new Categoria
            {
                Id = 3,
                Nome = "Acessórios",
                Descricao = "Fones de ouvido, carregadores, etc",
            },
        };
}
