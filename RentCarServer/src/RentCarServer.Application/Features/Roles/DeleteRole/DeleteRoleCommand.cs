using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.DeleteRole;

[Permission("role:delete")]
public sealed record DeleteRoleCommand(Guid Id) : IRequest<Result<string>>;
