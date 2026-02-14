using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Phnx.Shared.Constants;

namespace Phnx.Infrastructure.Persistence.Database;

public class PhnxDbContextFactory : IDesignTimeDbContextFactory<PhnxDbContext>
{
    public PhnxDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<PhnxDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(AppConstants.RUN_TIME_CONNECTION_STRING);
        return new PhnxDbContext(optionsBuilder.Options);
    }
}
