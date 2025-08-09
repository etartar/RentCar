using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Extras.CreateExtra;

[Permission("extra:create")]
public sealed record CreateExtraCommand(string Name, decimal Price, string Description, bool IsActive) : IRequest<Result<string>>;
