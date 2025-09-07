using BuidingBlock.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Module.Evently.Application.Event;
using Module.Evently.Presentation.ApiResult;

namespace Module.Evently.Presentation.Event;
internal sealed class GetEventHandlerByIdCommandEndPoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/event/{id:guid}", async (Guid id, ISender sender) =>
        {
            Result<GetEventHandlerResponseCommand> result = await sender.Send(new GetEventHandlerRequestCommand(id));
            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.Events);
    }
}
