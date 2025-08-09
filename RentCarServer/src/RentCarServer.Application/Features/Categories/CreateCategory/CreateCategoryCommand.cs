using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Categories.CreateCategory;

[Permission("category:create")]
public sealed record CreateCategoryCommand(string Name, bool IsActive) : IRequest<Result<string>>;
