using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.Roles;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.GetRole;

internal sealed class GetRoleQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRoleQuery, Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository
            .GetAllWithAudit()
            .MapToGet()
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null)
        {
            return Result<RoleDto>.Failure("Rol bulunamadı.");
        }

        return role;
    }
}
