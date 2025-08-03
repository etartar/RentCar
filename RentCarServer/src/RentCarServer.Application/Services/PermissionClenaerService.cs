using GenericRepository;
using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.Roles;
using System.Data;

namespace RentCarServer.Application.Services;

public sealed class PermissionClenaerService(IRoleRepository roleRepository, PermissionService permissionService, IUnitOfWork unitOfWork)
{
    public async Task CleanRemovedPermissionsFromRolesAsync(CancellationToken cancellationToken = default)
    {
        var currentPermissions = permissionService.GetAll();

        var roles = await roleRepository.GetAllWithTracking().ToListAsync(cancellationToken);

        foreach (var role in roles)
        {
            var currentPermissionsForRole = role.Permissions.Select(s => s.Value).ToList();

            var filteredPermissions = currentPermissionsForRole
                .Where(permission => currentPermissions.Contains(permission))
                .ToList();

            var permissions = filteredPermissions.Select(s => new Permission(s)).ToList();

            role.SetPermissions(permissions);
        }

        roleRepository.UpdateRange(roles);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
