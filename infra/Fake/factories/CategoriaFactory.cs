using TechStore.Core.Entities;

namespace TechStore.Infra.Fake.Factories;

public static class CategoriaFactory
{
    public static List<Categoria> Criar() =>
        new()
        {
            new Categoria("Periféricos", "Teclado, Mouse e etc"),
            new Categoria("Hardware", "Equipamentos essenciais"),
            new Categoria("Acessórios", "Fones de ouvido, carregadores, etc"),
            new Categoria("Software", "Programas em geral"),
        };
}
