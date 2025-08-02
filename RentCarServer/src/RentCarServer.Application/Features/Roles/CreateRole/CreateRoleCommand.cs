using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Roles.CreateRole;

[Permission("role:create")]
public sealed record CreateRoleCommand(string Name, bool IsActive) : IRequest<Result<string>>;
