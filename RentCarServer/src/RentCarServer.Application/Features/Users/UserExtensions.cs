using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Branches;
using RentCarServer.Domain.Roles;
using RentCarServer.Domain.Users;

namespace RentCarServer.Application.Features.Users;

public static class UserExtensions
{
    public static IQueryable<UserDto> MapTo(this IQueryable<EntityWithAuditDto<User>> entity, IQueryable<Role> roles, IQueryable<Branch> branches)
    {
        return entity
            .Join(roles, m => m.Entity.RoleId, m => m.Id, (e, role) => new { e.Entity, e.CreatedUser, e.UpdatedUser, Role = role })
            .Join(branches, m => m.Entity.BranchId, m => m.Id, (entity, branch) => new { entity.Entity, entity.CreatedUser, entity.UpdatedUser, entity.Role, Branch = branch })
            .Select(s => new UserDto
            {
                Id = s.Entity.Id,
                FirstName = s.Entity.FirstName.Value,
                LastName = s.Entity.LastName.Value,
                FullName = s.Entity.FullName.Value,
                Email = s.Entity.Email.Value,
                UserName = s.Entity.UserName.Value,
                BranchId = s.Entity.BranchId,
                BranchName = s.Branch.Name.Value,
                RoleId = s.Entity.RoleId,
                RoleName = s.Role.Name.Value,
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
