using RentCarServer.Domain.Extras;
using TS.MediatR;

namespace RentCarServer.Application.Features.Extras.GetAllExtra;

internal sealed class GetAllExtraQueryHandler(IExtraRepository extraRepository) : IRequestHandler<GetAllExtraQuery, IQueryable<ExtraDto>>
{
    public Task<IQueryable<ExtraDto>> Handle(GetAllExtraQuery request, CancellationToken cancellationToken)
    {
        var response = extraRepository
            .GetAllWithAudit()
            .MapTo();

        return Task.FromResult(response);
    }
}
