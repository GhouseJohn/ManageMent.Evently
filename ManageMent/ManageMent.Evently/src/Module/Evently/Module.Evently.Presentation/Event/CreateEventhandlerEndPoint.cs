using BuidingBlock.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Module.Evently.Application.Event;

namespace Module.Evently.Presentation.Event;
internal sealed class CreateEventhandlerEndPoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/event", async (Request request, [FromServices] ISender sender) =>
        {
            Result<CreateEventResult>? @event = await sender.Send(new CreateEventRequest(

                request.Title,
                request.Description,
                request.Location,
                request.StartsAtUtc,
                request.EndsAtUtc));
            return Results.Ok(@event);
        }).WithTags(Tags.Events);
    }
}
internal sealed class Request
{
    public Guid CategoryId { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public string Location { get; init; }

    public DateTime StartsAtUtc { get; init; }

    public DateTime EndsAtUtc { get; init; }
}

