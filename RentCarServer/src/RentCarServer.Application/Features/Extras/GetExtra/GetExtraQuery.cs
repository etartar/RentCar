using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Extras.GetExtra;

[Permission("extra:view")]
public sealed record GetExtraQuery(Guid Id) : IRequest<Result<ExtraDto>>;
