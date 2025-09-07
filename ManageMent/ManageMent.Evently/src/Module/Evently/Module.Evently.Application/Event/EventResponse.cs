namespace Module.Evently.Application.Event;


#if false
public sealed record EventResponse(
    Guid Id,
    Guid CategoryId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc);
#endif

public sealed record EventResponse
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public DateTime StartsAtUtc { get; init; }
    public DateTime? EndsAtUtc { get; init; }

    // Dapper needs this
    public EventResponse() { }
}


