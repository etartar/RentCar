using RentCarServer.Application.Attributes;
using TS.MediatR;

namespace RentCarServer.Application.Features.Roles.GetAllRole;

[Permission("role:view")]
public sealed record GetAllRoleQuery() : IRequest<IQueryable<RoleDto>>;
