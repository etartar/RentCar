using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.ProtectionPackages.DeleteProtectionPackage;

[Permission("protection_package:delete")]
public sealed record DeleteProtectionPackageCommand(Guid Id) : IRequest<Result<string>>;
