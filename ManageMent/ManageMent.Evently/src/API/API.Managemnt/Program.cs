using API.Managemnt.Extensions;
using API.Managemnt.Middleware;
using BuidingBlock.Application;
using BuidingBlock.Infrastructure;
using BuidingBlock.Presentation.Endpoints;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Module.Evently.InfraStructure;
using Module.Ticketing.InfraStructure;
using Moduler.user.InfraStructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplication([
    Module.Evently.Application.AssemblyReference.Assembly,
    Module.User.Application.AssemblyReference.Assembly,
    Module.Ticketing.Application.AssemblyReference.Assembly
    ]);

#region ExceptionalHandling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
#endregion

#region Connectionstring
string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;
builder.Services.AddInfrastructure(databaseConnectionString, redisConnectionString);

builder.Configuration.AddModuleConfiguration(["events", "users", "ticketing"]);


builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);
#endregion

#region HealthStatus
builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString);
#endregion


WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.MapEndpoints();
app.Run();
