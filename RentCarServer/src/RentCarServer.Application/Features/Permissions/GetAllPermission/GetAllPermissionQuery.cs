using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Permissions.GetAllPermission;

[Permission("permission:view")]
public sealed record GetAllPermissionQuery : IRequest<Result<List<string>>>;
