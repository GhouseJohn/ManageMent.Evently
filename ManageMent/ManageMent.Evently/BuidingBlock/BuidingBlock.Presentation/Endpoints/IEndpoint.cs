using Microsoft.AspNetCore.Routing;

namespace BuidingBlock.Presentation.Endpoints;
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

