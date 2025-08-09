using GenericRepository;
using RentCarServer.Domain.ProtectionPackages;
using RentCarServer.Domain.ProtectionPackages.ValueObjects;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.ProtectionPackages.CreateProtectionPackage;

public sealed class CreateProtectionPackageCommandHandler(
    IProtectionPackageRepository protectionPackageRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProtectionPackageCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProtectionPackageCommand request, CancellationToken cancellationToken)
    {
        var nameIsExists = await protectionPackageRepository.AnyAsync(b => b.Name.Value == request.Name, cancellationToken);

        if (nameIsExists)
        {
            return Result<string>.Failure($"Bu paket güvence adı '{request.Name}' sistemde kayıtlıdır.");
        }

        Name name = new Name(request.Name);
        Price price = new Price(request.Price);
        IsRecommended isRecommended = new IsRecommended(request.IsRecommended);
        OrderNumber orderNumber = new OrderNumber(request.OrderNumber);
        List<ProtectionCoverage> coverages = request.Coverages
            .Select(x => new ProtectionCoverage(x))
            .ToList();

        ProtectionPackage protectionPackage = new ProtectionPackage(
            name,
            price,
            isRecommended,
            orderNumber,
            coverages,
            request.IsActive);

        await protectionPackageRepository.AddAsync(protectionPackage, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Paket güvencesi başarılı şekilde kayıt edildi.";
    }
}