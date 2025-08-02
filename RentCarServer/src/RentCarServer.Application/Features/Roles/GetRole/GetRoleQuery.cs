using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.GetRole;

[Permission("role:view")]
public sealed record GetRoleQuery(Guid Id) : IRequest<Result<RoleDto>>;
