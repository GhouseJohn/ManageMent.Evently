using Microsoft.EntityFrameworkCore;
using Module.User.Domain.Users;
using Moduler.user.InfraStructure.Database;

namespace Moduler.user.InfraStructure.Users;
internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public async Task<UserProperties?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(UserProperties user)
    {
        context.Users.Add(user);
    }
}
