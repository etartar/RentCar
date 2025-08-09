using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Categories;

namespace RentCarServer.Application.Features.Categories;

public static class CategoryExtensions
{
    public static IQueryable<CategoryDto> MapTo(this IQueryable<EntityWithAuditDto<Category>> entity)
    {
        return entity
            .Select(s => new CategoryDto
            {
                Id = s.Entity.Id,
                Name = s.Entity.Name.Value,
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
