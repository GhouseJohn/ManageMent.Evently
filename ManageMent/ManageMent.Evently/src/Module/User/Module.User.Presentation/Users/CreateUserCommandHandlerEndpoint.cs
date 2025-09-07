using BuidingBlock.Domain;
using BuidingBlock.Presentation.ApiResult;
using BuidingBlock.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Module.User.Application.Users;

namespace Module.User.Presentation.Users;
internal sealed class CreateUserCommandHandlerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/user", async (CreateUserRequest request, [FromServices] ISender sender) =>
        {
            Result<CreateuserCommandResponse> result = await sender.Send(new CreateuserCommandRequest(request.FirstName,
                                                         request.Email, request.LastName));
            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.Users);
    }
}

public class CreateUserRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


