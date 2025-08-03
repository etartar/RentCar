using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.UpdateRolePermission;

public sealed record UpdateRolePermissionCommand(Guid RoleId, List<string> Permissions) : IRequest<Result<string>>;
