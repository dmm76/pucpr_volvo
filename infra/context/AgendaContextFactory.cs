using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechStore.infra.context;

public class AgendaContextFactory : IDesignTimeDbContextFactory<AgendaContext>
{
    public AgendaContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AgendaContext>();

        var cs =
            "Server=.\\SQLEXPRESS;Database=TechStore;Trusted_Connection=True;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(cs);

        return new AgendaContext(optionsBuilder.Options);
    }
}
