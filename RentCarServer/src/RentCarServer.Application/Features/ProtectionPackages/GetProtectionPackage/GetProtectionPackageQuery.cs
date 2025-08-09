using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.ProtectionPackages.GetProtectionPackage;

[Permission("protection_package:view")]
public sealed record GetProtectionPackageQuery(Guid Id) : IRequest<Result<ProtectionPackageDto>>;