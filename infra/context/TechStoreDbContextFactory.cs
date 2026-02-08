using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechStore.Infra.Context;

public class TechStoreDbContextFactory : IDesignTimeDbContextFactory<TechStoreDbContext>
{
    public TechStoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TechStoreDbContext>();

        var cs =
            "Server=.\\SQLEXPRESS;Database=TechStore;Trusted_Connection=True;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(cs);
        return new TechStoreDbContext(optionsBuilder.Options);
    }
}
