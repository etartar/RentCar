using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Categories.DeleteCategory;

[Permission("category:delete")]
public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Result<string>>;
