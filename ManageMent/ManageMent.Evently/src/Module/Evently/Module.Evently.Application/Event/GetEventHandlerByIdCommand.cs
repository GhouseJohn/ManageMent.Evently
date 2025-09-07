using System.Data.Common;
using BuidingBlock.Application.Data;
using BuidingBlock.Application.Messaging;
using BuidingBlock.Domain;
using Dapper;

namespace Module.Evently.Application.Event;

public sealed record GetEventHandlerRequestCommand(Guid EventId)
                                                : IQuery<Result<GetEventHandlerResponseCommand>>;
public sealed record GetEventHandlerResponseCommand(EventResponse EventResponse);
internal sealed class GetEventHandlerCommand(IDbConnectionFactory dbConnectionFactory)
                        : IQueryHandler<GetEventHandlerRequestCommand, Result<GetEventHandlerResponseCommand>>
{
    public async Task<Result<GetEventHandlerResponseCommand>> Handle(GetEventHandlerRequestCommand request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql =
           $"""  
            SELECT  
                e.id AS {nameof(EventResponse.Id)},  
                e.category_id AS {nameof(EventResponse.CategoryId)},  
                e.title AS {nameof(EventResponse.Title)},  
                e.description AS {nameof(EventResponse.Description)},  
                e.location AS {nameof(EventResponse.Location)},  
                e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},  
                e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)}  
            FROM events.events e  
            WHERE e.id = @EventId  
            """;
        EventResponse? eventResponse = await connection.QuerySingleOrDefaultAsync<EventResponse>(sql, new { request.EventId });

        if (eventResponse is null)
        {
            return Result.Failure<GetEventHandlerResponseCommand>(Error.Failure($"The Event Id {request.EventId} not found",
                                                                        "Given Id Not Found"));
        }

        return Result.Success(new GetEventHandlerResponseCommand(eventResponse));

    }
}
