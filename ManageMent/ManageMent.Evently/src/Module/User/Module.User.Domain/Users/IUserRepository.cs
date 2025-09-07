namespace Module.User.Domain.Users;

public interface IUserRepository
{
    Task<UserProperties?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(UserProperties user);
}
