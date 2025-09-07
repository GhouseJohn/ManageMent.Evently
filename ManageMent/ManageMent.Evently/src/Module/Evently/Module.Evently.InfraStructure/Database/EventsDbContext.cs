using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Module.Evently.Application;
using Module.Evently.Domain.Events;

namespace Module.Evently.InfraStructure.Database;
public sealed class EventsDbContext : DbContext, IUnitOfWork
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }
    internal DbSet<ModelEvent> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);
    }
}


public class EventsDbContextFactory : IDesignTimeDbContextFactory<EventsDbContext>
{
    public EventsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EventsDbContext>();

        optionsBuilder
            .UseNpgsql("Host=evently.database;Port=5432;Database=evently;Username=postgres;Password=postgres;Include Error Detail=true")
            .UseSnakeCaseNamingConvention();

        return new EventsDbContext(optionsBuilder.Options);
    }
}
