using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Users.DeleteUser;

[Permission("user:delete")]
public sealed record DeleteUserCommand(Guid Id) : IRequest<Result<string>>;