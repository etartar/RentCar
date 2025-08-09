using GenericRepository;
using RentCarServer.Domain.ProtectionPackages;
using RentCarServer.Domain.ProtectionPackages.ValueObjects;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.ProtectionPackages.UpdateProtectionPackage;

internal sealed class UpdateProtectionPackageCommandHandler(
    IProtectionPackageRepository protectionPackageRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProtectionPackageCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProtectionPackageCommand request, CancellationToken cancellationToken)
    {
        var protectionPackage = await protectionPackageRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (protectionPackage is null)
        {
            return Result<string>.Failure("Güvence Paketi bulunamadı.");
        }

        if (protectionPackage.Name.Value != request.Name)
        {
            var nameExists = await protectionPackageRepository.AnyAsync(x => x.Name.Value == request.Name, cancellationToken);

            if (nameExists)
            {
                return Result<string>.Failure("Bu güvence paketi daha önce tanımlanmış.");
            }
        }

        var coverages = request.Coverages
            .Select(x => new ProtectionCoverage(x))
            .ToList();

        protectionPackage.SetName(new Name(request.Name));
        protectionPackage.SetPrice(new Price(request.Price));
        protectionPackage.SetIsRecommended(new IsRecommended(request.IsRecommended));
        protectionPackage.SetOrderNumber(new OrderNumber(request.OrderNumber));
        protectionPackage.SetCoverages(coverages);
        protectionPackage.SetStatus(request.IsActive);

        protectionPackageRepository.Update(protectionPackage);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Güvence Paketi bilgisi başarılı şekilde güncellendi.";
    }
}