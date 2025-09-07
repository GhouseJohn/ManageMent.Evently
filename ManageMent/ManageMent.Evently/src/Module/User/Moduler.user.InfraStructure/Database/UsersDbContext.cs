using Microsoft.EntityFrameworkCore;
using Module.User.Application.Abstractions.Data;
using Module.User.Domain.Users;
using Module.User.Infrastructure.Database;

namespace Moduler.user.InfraStructure.Database;
public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<UserProperties> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
    }
}
