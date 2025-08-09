using RentCarServer.Application.Attributes;
using TS.MediatR;

namespace RentCarServer.Application.Features.ProtectionPackages.GetAllProtectionPackage;

[Permission("protection_package:view")]
public sealed record GetAllProtectionPackageQuery() : IRequest<IQueryable<ProtectionPackageDto>>;
