using RentCarServer.Domain.Roles;
using TS.MediatR;

namespace RentCarServer.Application.Features.Roles.GetAllRole;

internal sealed class GetAllRoleQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetAllRoleQuery, IQueryable<RoleDto>>
{
    public Task<IQueryable<RoleDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var response = roleRepository
            .GetAllWithAudit()
            .MapTo();

        return Task.FromResult(response);
    }
}