using RentCarServer.Application.Attributes;
using TS.MediatR;

namespace RentCarServer.Application.Features.Extras.GetAllExtra;

[Permission("extra:view")]
public sealed record GetAllExtraQuery() : IRequest<IQueryable<ExtraDto>>;
