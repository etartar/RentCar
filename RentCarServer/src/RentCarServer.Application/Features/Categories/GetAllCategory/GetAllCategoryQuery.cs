using RentCarServer.Application.Attributes;
using TS.MediatR;

namespace RentCarServer.Application.Features.Categories.GetAllCategory;

[Permission("category:view")]
public sealed record GetAllCategoryQuery() : IRequest<IQueryable<CategoryDto>>;
