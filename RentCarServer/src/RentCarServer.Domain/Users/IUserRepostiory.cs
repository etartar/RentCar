using RentCarServer.Domain.Abstractions;

namespace RentCarServer.Domain.Users;

public interface IUserRepostiory : IAuditableRepository<User>
{
}
