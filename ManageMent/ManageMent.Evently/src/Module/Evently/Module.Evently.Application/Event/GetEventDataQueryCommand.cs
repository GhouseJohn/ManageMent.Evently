using System.Data.Common;
using BuidingBlock.Application.Data;
using BuidingBlock.Application.Messaging;
using BuidingBlock.Domain;
using Dapper;

namespace Module.Evently.Application.Event;

public sealed record GetEventDataQuery : IQuery<Result<GetEventDataResponse>>;
public sealed record GetEventDataResponse(List<EventResponse> EventResponses);
internal sealed class GetEventDataQueryCommand(IDbConnectionFactory dbConnectionFactory)
                                    : IQueryHandler<GetEventDataQuery, Result<GetEventDataResponse>>
{
    public async Task<Result<GetEventDataResponse>> Handle(GetEventDataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            await using DbConnection sqlcon = await dbConnectionFactory.OpenConnectionAsync();
            const string sql = """  
           SELECT *
           FROM events.events  
           """;
            var eventResponses = (await sqlcon.QueryAsync<EventResponse>(sql)).ToList();
            if (!eventResponses.Any())
            {
                return Result.Failure<GetEventDataResponse>(Error.Failure("No events found", "No Data"));
            }
            return Result.Success(new GetEventDataResponse(eventResponses));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
