using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.UpdateRole;

[Permission("role:edit")]
public sealed record UpdateRoleCommand(Guid Id, string Name, bool IsActive) : IRequest<Result<string>>;
