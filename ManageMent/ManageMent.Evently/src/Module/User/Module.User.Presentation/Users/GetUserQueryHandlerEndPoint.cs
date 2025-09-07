using BuidingBlock.Domain;
using BuidingBlock.Presentation.ApiResult;
using BuidingBlock.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Module.User.Application.Users;

namespace Module.User.Presentation.Users;


internal sealed class GetUserQueryHandlerEndPoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/User", async (Guid userId, ISender sender) =>
        {
            Result<GetUserQueryResponse> result = await sender.Send(new GetUserQueryRequest(userId));
            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.Users);
    }
}

