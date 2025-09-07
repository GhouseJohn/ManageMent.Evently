using BuidingBlock.Application.Data;
using BuidingBlock.Infrastructure.Data;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Evently.Application;
using Module.Evently.Domain.Events;
using Module.Evently.InfraStructure.Database;
using Module.Evently.InfraStructure.Events;
using Module.Evently.Presentation;


namespace Module.Evently.InfraStructure;
public static class EventsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        EventEndpoints.MapEndpoints(app);
    }
    public static IServiceCollection AddEventsModule(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<EventsDbContext>(options =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
    }
}
