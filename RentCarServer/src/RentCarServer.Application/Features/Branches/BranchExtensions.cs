using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Branches;

namespace RentCarServer.Application.Features.Branches;

public static class BranchExtensions
{
    public static IQueryable<BranchDto> MapTo(this IQueryable<EntityWithAuditDto<Branch>> entity)
    {
        return entity
            .Select(s => new BranchDto
            {
                Id = s.Entity.Id,
                Name = s.Entity.Name.Value,
                Address = s.Entity.Address,
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
