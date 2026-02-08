using Microsoft.EntityFrameworkCore;
using TechStore.Core.Entities;

namespace TechStore.infra.context;

public class AgendaContext : DbContext
{
    public AgendaContext(DbContextOptions<AgendaContext> options)
        : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Cliente>()
            .HasMany(c => c.Enderecos)
            .WithOne(e => e.Cliente)
            .HasForeignKey(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        // opcional:
        // modelBuilder.Entity<Cliente>()
        //     .HasIndex(c => c.Login)
        //     .IsUnique();
    }
}
