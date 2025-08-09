using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Extras;

namespace RentCarServer.Application.Features.Extras;

public static class ExtraExtensions
{
    public static IQueryable<ExtraDto> MapTo(this IQueryable<EntityWithAuditDto<Extra>> entity)
    {
        return entity
            .Select(s => new ExtraDto
            {
                Id = s.Entity.Id,
                Name = s.Entity.Name.Value,
                Price = s.Entity.Price.Value,
                Description = s.Entity.Description.Value,
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
