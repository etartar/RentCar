using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Extras.UpdateExtra;

[Permission("extra:edit")]
public sealed record UpdateExtraCommand(Guid Id, string Name, decimal Price, string Description, bool IsActive) : IRequest<Result<string>>;
