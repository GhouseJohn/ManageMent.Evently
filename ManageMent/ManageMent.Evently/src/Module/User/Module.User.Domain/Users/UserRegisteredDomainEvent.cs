using BuidingBlock.Domain;

namespace Module.User.Domain.Users;
public sealed class UserRegisteredDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}

