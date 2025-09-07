using BuidingBlock.Domain;

namespace Module.Evently.Domain;
public sealed class EventCreatedDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
