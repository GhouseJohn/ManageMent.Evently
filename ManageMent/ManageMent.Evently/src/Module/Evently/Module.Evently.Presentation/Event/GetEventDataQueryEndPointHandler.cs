using BuidingBlock.Domain;
using BuidingBlock.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Module.Evently.Application.Event;

namespace Module.Evently.Presentation.Event;
internal sealed class GetEventDataQueryEndPointHandler : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/eventData", async (ISender sender) =>
        {
            Result<GetEventDataResponse> result = await sender.Send(new GetEventDataQuery());
            return Results.Ok(result);
        }).WithName(Tags.Events).WithTags(Tags.Events);
    }
}
