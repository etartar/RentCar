using RentCarServer.Application.Services;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Permissions.GetAllPermission;

internal sealed class GetAllPermissionQueryHandler(PermissionService permissionService) : IRequestHandler<GetAllPermissionQuery, Result<List<string>>>
{
    public Task<Result<List<string>>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
    {
        var permissions = permissionService.GetAll();

        return Task.FromResult(Result<List<string>>.Succeed(permissions));
    }
}
