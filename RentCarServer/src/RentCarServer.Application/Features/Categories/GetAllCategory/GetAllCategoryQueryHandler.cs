using RentCarServer.Domain.Categories;
using TS.MediatR;

namespace RentCarServer.Application.Features.Categories.GetAllCategory;

internal sealed class GetAllCategoryQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategoryQuery, IQueryable<CategoryDto>>
{
    public Task<IQueryable<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var response = categoryRepository
            .GetAllWithAudit()
            .MapTo();

        return Task.FromResult(response);
    }
}
