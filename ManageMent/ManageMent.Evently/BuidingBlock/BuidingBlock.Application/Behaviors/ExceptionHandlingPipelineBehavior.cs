using BuidingBlock.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuidingBlock.Application.Behaviors;
internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
#pragma warning disable CA2016
            return await next();
#pragma warning restore CA2016

        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception for {RequestName}", typeof(TRequest).Name);

            throw new EventlyException(typeof(TRequest).Name, innerException: exception);
        }
    }
}

