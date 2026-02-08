using Microsoft.EntityFrameworkCore;
using TechStore.Core.Entities;

namespace TechStore.Infra.Context;

public class TechStoreDbContext : DbContext
{
    public TechStoreDbContext(DbContextOptions<TechStoreDbContext> options)
        : base(options) { }

    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ============= Categoria / Produto =============
        modelBuilder.Entity<Categoria>(e =>
        {
            e.ToTable("Categorias");
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).HasMaxLength(120).IsRequired();
            e.Property(x => x.Descricao).HasMaxLength(500);
        });

        modelBuilder.Entity<Produto>(e =>
        {
            e.ToTable("Produtos");
            e.HasKey(x => x.Id);

            e.Property(x => x.Nome).HasMaxLength(160).IsRequired();
            e.Property(x => x.Descricao).HasMaxLength(1000);

            e.Property(x => x.PrecoAtual).HasPrecision(18, 2);
            e.HasIndex(x => x.Nome).IsUnique();

            e.HasOne<Categoria>()
                .WithMany()
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ============= User / Cliente / Endereco =============
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasKey(x => x.Id);

            e.Property(x => x.Login).HasMaxLength(60).IsRequired();
            e.Property(x => x.Email).HasMaxLength(140).IsRequired();
            e.Property(x => x.SenhaHash).HasMaxLength(500).IsRequired();

            e.HasIndex(x => x.Login).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Cliente>(e =>
        {
            e.ToTable("Clientes");
            e.HasKey(x => x.Id);

            e.Property(x => x.Nome).HasMaxLength(140).IsRequired();
            e.Property(x => x.Telefone).HasMaxLength(30).IsRequired();

            e.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Cliente>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(x => x.UserId).IsUnique();

            // coleção read-only com backing field
            e.Navigation(x => x.Enderecos).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<Endereco>(e =>
        {
            e.ToTable("Enderecos");
            e.HasKey(x => x.Id);

            e.Property(x => x.Descricao).HasMaxLength(200).IsRequired();
            e.Property(x => x.Telefone).HasMaxLength(30);
            e.Property(x => x.CEP).HasMaxLength(20);

            e.HasOne(x => x.Cliente)
                .WithMany(x => x.Enderecos)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ============= Pedido / ItemPedido =============
        modelBuilder.Entity<Pedido>(e =>
        {
            e.ToTable("Pedidos");
            e.HasKey(x => x.Id);

            e.Property(x => x.Status).IsRequired();
            e.Property(x => x.Total).HasPrecision(18, 2);

            e.Property(x => x.CustomerNameSnapshot).HasMaxLength(140);
            e.Property(x => x.ShippingAddressSnapshot).HasMaxLength(400);

            e.HasOne<Cliente>()
                .WithMany()
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            e.Navigation(x => x.Itens).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<ItemPedido>(e =>
        {
            e.ToTable("ItensPedido");
            e.HasKey(x => x.Id);

            e.Property(x => x.NomeProdutoSnapshot).HasMaxLength(160).IsRequired();
            e.Property(x => x.PrecoUnitarioSnapshot).HasPrecision(18, 2);
            e.Property(x => x.SubTotal).HasPrecision(18, 2);

            e.HasOne<Pedido>()
                .WithMany(x => x.Itens)
                .HasForeignKey(x => x.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne<Produto>()
                .WithMany()
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
