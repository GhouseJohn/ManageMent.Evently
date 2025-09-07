using Microsoft.AspNetCore.Routing;
using Module.Evently.Presentation.Event;

namespace Module.Evently.Presentation;
public static class EventEndpoints123
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateEventhandlerEndPoint.MapEndpoint(app);
        GetEventHandlerByIdCommandEndPoint.MapEndpoint(app);
    }
}
