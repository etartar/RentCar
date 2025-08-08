using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.Branches;
using RentCarServer.Domain.Roles;
using RentCarServer.Domain.Users;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Users.GetUser;

internal sealed class GetUserQueryHandler(
    IUserRepostiory userRepostiory,
    IRoleRepository roleRepository,
    IBranchRepository branchRepository) : IRequestHandler<GetUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var roles = roleRepository.GetAll();

        var branches = branchRepository.GetAll();

        var user = await userRepostiory
            .GetAllWithAudit()
            .MapTo(roles, branches)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result<UserDto>.Failure("Kullanıcı bulunamadı.");
        }

        return user;
    }
}
