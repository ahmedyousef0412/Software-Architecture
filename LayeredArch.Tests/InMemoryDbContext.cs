using LayeredArch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace LayeredArch.Tests;

internal class InMemoryDbContext : ApplicationDbContext
{


    public InMemoryDbContext() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options)
    { }

    public override void Dispose()
    {
        Database.EnsureDeleted();
    }
}
