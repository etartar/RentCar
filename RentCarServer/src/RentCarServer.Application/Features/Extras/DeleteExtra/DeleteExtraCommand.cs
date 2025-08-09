using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Extras.DeleteExtra;

[Permission("extra:delete")]
public sealed record DeleteExtraCommand(Guid Id) : IRequest<Result<string>>;
