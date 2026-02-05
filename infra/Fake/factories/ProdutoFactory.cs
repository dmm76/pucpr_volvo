using TechStore.Core.Entities;

namespace TechStore.Infra.Fake.Factories;

public static class ProdutoFactory
{
    public static List<Produto> Criar() =>
        new()
        {
            new Produto(1, "Mouse Gamer", 199.90m, 30, "RGB 16000 DPI"),
            new Produto(1, "Teclado Mecânico", 349.90m, 20, "Switch Blue"),
            new Produto(2, "SSD 1TB NVMe", 499.90m, 15, "Leitura 3500MB/s"),
            new Produto(2, "Memória RAM 16GB DDR4", 279.90m, 25, "3200MHz"),
            new Produto(3, "Headset USB", 159.90m, 18, "Som 7.1 virtual"),
            new Produto(3, "Carregador USB-C 65W", 129.90m, 40, "PD / GaN"),
            new Produto(4, "Antivírus 1 ano", 99.90m, 999, "Licença digital"),
        };
}
