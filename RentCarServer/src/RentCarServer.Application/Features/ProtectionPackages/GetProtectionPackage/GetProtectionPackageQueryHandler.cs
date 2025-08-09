using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.ProtectionPackages;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.ProtectionPackages.GetProtectionPackage;

internal sealed class GetProtectionPackageQueryHandler(IProtectionPackageRepository protectionPackageRepository) : IRequestHandler<GetProtectionPackageQuery, Result<ProtectionPackageDto>>
{
    public async Task<Result<ProtectionPackageDto>> Handle(GetProtectionPackageQuery request, CancellationToken cancellationToken)
    {
        var protectionPackage = await protectionPackageRepository
            .GetAllWithAudit()
            .MapTo()
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (protectionPackage is null)
        {
            return Result<ProtectionPackageDto>.Failure("Paket Güvencesi bulunamadı.");
        }

        return protectionPackage;
    }
}
