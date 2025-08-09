using RentCarServer.Domain.ProtectionPackages;
using TS.MediatR;

namespace RentCarServer.Application.Features.ProtectionPackages.GetAllProtectionPackage;

internal sealed class GetAllProtectionPackageQueryHandler(IProtectionPackageRepository protectionPackageRepository) : IRequestHandler<GetAllProtectionPackageQuery, IQueryable<ProtectionPackageDto>>
{
    public Task<IQueryable<ProtectionPackageDto>> Handle(
        GetAllProtectionPackageQuery request,
        CancellationToken cancellationToken)
    {
        var response = protectionPackageRepository
            .GetAllWithAudit()
            .MapTo();

        return Task.FromResult(response);
    }
}