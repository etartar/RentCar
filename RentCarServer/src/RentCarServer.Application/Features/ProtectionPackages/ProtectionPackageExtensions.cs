using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.ProtectionPackages;

namespace RentCarServer.Application.Features.ProtectionPackages;

public static class ProtectionPackageExtensions
{
    public static IQueryable<ProtectionPackageDto> MapTo(this IQueryable<EntityWithAuditDto<ProtectionPackage>> entity)
    {
        return entity
            .Select(s => new ProtectionPackageDto
            {
                Id = s.Entity.Id,
                Name = s.Entity.Name.Value,
                Price = s.Entity.Price.Value,
                IsRecommended = s.Entity.IsRecommended.Value,
                OrderNumber = s.Entity.OrderNumber.Value,
                CoverageCount = s.Entity.Coverages.Count,
                Coverages = s.Entity.Coverages.Select(c => c.Name).ToList(),
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
