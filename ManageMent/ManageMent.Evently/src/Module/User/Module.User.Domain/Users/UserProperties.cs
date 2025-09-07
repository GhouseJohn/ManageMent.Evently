using BuidingBlock.Domain;

namespace Module.User.Domain.Users;
public sealed class UserProperties : Entity
{
    private UserProperties() { }
    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public static UserProperties Create(string email, string firstName, string lastName)
    {
        var user = new UserProperties
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
        };
        user.Raise(new UserRegisteredDomainEvent(user.Id));
        return user;
    }
    public void Update(string firstName, string lastName)
    {
        if (FirstName == firstName && LastName == lastName)
        {
            return;
        }

        FirstName = firstName;
        LastName = lastName;

        Raise(new UserProfileUpdatedDomainEvent(Id, FirstName, LastName));
    }
}

