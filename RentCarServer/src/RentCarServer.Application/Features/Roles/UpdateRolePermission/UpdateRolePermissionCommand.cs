using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.UpdateRolePermission;

[Permission("role:update_permissions")]
public sealed record UpdateRolePermissionCommand(Guid RoleId, List<string> Permissions) : IRequest<Result<string>>;
