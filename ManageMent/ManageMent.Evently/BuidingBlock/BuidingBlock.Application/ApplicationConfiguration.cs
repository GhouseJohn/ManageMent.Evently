using System.Reflection;
using BuidingBlock.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace BuidingBlock.Application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
                                                                     Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
        });


        return services;
    }
}
