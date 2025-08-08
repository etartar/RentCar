using RentCarServer.Domain.Users;
using RentCarServer.Infrastructure.Abstractions;
using RentCarServer.Infrastructure.Context;

namespace RentCarServer.Infrastructure.Repositories;

internal sealed class UserRepository : AuditableRepository<User, ApplicationDbContext>, IUserRepostiory
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
