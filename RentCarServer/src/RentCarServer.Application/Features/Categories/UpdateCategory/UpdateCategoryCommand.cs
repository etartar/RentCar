using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Categories.UpdateCategory;

[Permission("category:edit")]
public sealed record UpdateCategoryCommand(Guid Id, string Name, bool IsActive) : IRequest<Result<string>>;
