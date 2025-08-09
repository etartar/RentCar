using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.Extras;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Extras.GetExtra;

internal sealed class GetExtraQueryHandler(IExtraRepository extraRepository) : IRequestHandler<GetExtraQuery, Result<ExtraDto>>
{
    public async Task<Result<ExtraDto>> Handle(GetExtraQuery request, CancellationToken cancellationToken)
    {
        var extra = await extraRepository
            .GetAllWithAudit()
            .MapTo()
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (extra is null)
        {
            return Result<ExtraDto>.Failure("Ekstra bulunamadı.");
        }

        return extra;
    }
}
