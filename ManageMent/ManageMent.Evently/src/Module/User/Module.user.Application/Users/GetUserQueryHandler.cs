using System.Data.Common;
using BuidingBlock.Application.Data;
using BuidingBlock.Application.Messaging;
using BuidingBlock.Domain;
using Dapper;

namespace Module.User.Application.Users;

public sealed record GetUserQueryRequest(Guid @UserId) : IQuery<Result<GetUserQueryResponse>>;
public sealed record GetUserQueryResponse(UserResponse OuserResponse);

internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetUserQueryRequest, Result<GetUserQueryResponse>>
{
    public async Task<Result<GetUserQueryResponse>> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql = $"""  
            SELECT  
                e.Id AS {nameof(UserResponse.Id)},  
                e.Email AS {nameof(UserResponse.Email)},  
                e.FirstName AS {nameof(UserResponse.FirstName)},  
                e.LastName AS {nameof(UserResponse.LastName)} 
            FROM users.users
            WHERE e.Id = @UserId  
            """;
        UserResponse? userResponse = await connection.QueryFirstOrDefaultAsync<UserResponse>(sql, new { request.UserId });
        if (userResponse is null)
        {
            return Result.Failure<GetUserQueryResponse>(Error.Failure($"The User Id {request.UserId} not found",
                                                                                    "Given Id Not Found"));
        }
        else
        {
            return Result.Success(new GetUserQueryResponse(userResponse));
        }
    }
}
