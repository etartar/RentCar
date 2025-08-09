using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.ProtectionPackages.UpdateProtectionPackage;

[Permission("protection_package:edit")]
public sealed record UpdateProtectionPackageCommand(
    Guid Id,
    string Name,
    decimal Price,
    bool IsRecommended,
    int OrderNumber,
    List<string> Coverages,
    bool IsActive) : IRequest<Result<string>>;
