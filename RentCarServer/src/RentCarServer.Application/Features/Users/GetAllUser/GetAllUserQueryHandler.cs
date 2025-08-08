using RentCarServer.Application.Services;
using RentCarServer.Domain.Branches;
using RentCarServer.Domain.Roles;
using RentCarServer.Domain.Users;
using TS.MediatR;

namespace RentCarServer.Application.Features.Users.GetAllUser;

internal sealed class GetAllUserQueryHandler(
    IUserContext userContext,
    IUserRepostiory userRepostiory,
    IRoleRepository roleRepository,
    IBranchRepository branchRepository) : IRequestHandler<GetAllUserQuery, IQueryable<UserDto>>
{
    public Task<IQueryable<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var roles = roleRepository.GetAll();

        var branches = branchRepository.GetAll();

        var response = userRepostiory
            .GetAllWithAudit()
            .MapTo(roles, branches);

        if (userContext.GetRoleName() != "SysAdmin")
        {
            response = response.Where(x => x.BranchId == userContext.GetBranchId());
        }

        return Task.FromResult(response);
    }
}
