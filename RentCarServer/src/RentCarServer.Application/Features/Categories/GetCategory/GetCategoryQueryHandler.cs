using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.Categories;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Categories.GetCategory;

internal sealed class GetCategoryQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository
            .GetAllWithAudit()
            .MapTo()
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            return Result<CategoryDto>.Failure("Kategori bulunamadý.");
        }

        return category;
    }
}
