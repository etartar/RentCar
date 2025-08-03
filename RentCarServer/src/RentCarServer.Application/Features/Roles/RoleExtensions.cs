using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Roles;

namespace RentCarServer.Application.Features.Roles;

public static class RoleExtensions
{
    public static IQueryable<RoleDto> MapTo(this IQueryable<EntityWithAuditDto<Role>> entity)
    {
        return entity
            .Select(s => new RoleDto
            {
                Id = s.Entity.Id,
                Name = s.Entity.Name.Value,
                PermissionCount = s.Entity.Permissions.Count,
                IsActive = s.Entity.IsActive,
                CreatedAt = s.Entity.CreatedAt,
                CreatedBy = s.Entity.CreatedBy,
                UpdatedAt = s.Entity.UpdatedAt,
                UpdatedBy = s.Entity.UpdatedBy == null ? null : s.Entity.UpdatedBy.Value,
                CreatedFullName = s.CreatedUser.FullName.Value,
                UpdatedFullName = s.UpdatedUser == null ? null : s.UpdatedUser.FullName.Value,
            })
            .AsQueryable();
    }

    public static IQueryable<RoleDto> MapToGet(this IQueryable<EntityWithAuditDto<Role>> entity)
    {
        return entity
            .Select(s => new RoleDto
            {
                Id = s.Entity.Id,
                Name = s.Entity.Name.Value,
                PermissionCount = s.Entity.Permissions.Count,
                Permissions = s.Entity.Permissions.Select(p => p.Value).ToList(),
                IsActive = s.Entity.IsActive,
                CreatedAt = s.Entity.CreatedAt,
                CreatedBy = s.Entity.CreatedBy,
                UpdatedAt = s.Entity.UpdatedAt,
                UpdatedBy = s.Entity.UpdatedBy == null ? null : s.Entity.UpdatedBy.Value,
                CreatedFullName = s.CreatedUser.FullName.Value,
                UpdatedFullName = s.UpdatedUser == null ? null : s.UpdatedUser.FullName.Value,
            })
            .AsQueryable();
    }
}
