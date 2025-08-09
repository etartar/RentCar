using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.ProtectionPackages.CreateProtectionPackage;

[Permission("protection_package:create")]
public sealed record CreateProtectionPackageCommand(
    string Name,
    decimal Price,
    bool IsRecommended,
    int OrderNumber,
    List<string> Coverages,
    bool IsActive) : IRequest<Result<string>>;
