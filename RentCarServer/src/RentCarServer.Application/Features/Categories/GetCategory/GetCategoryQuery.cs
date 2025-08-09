using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Categories.GetCategory;

[Permission("category:view")]
public sealed record GetCategoryQuery(Guid Id) : IRequest<Result<CategoryDto>>;
