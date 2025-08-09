using GenericRepository;
using RentCarServer.Domain.ProtectionPackages;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.ProtectionPackages.DeleteProtectionPackage;

internal sealed class DeleteProtectionPackageCommandHandler(
    IProtectionPackageRepository protectionPackageRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteProtectionPackageCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProtectionPackageCommand request, CancellationToken cancellationToken)
    {
        var protectionPackage = await protectionPackageRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (protectionPackage is null)
        {
            return Result<string>.Failure("Güvence Paketi bulunamadı.");
        }
        protectionPackage.Delete();

        protectionPackageRepository.Update(protectionPackage);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Güvence Paketi başarılı bir şekilde silindi.";
    }
}