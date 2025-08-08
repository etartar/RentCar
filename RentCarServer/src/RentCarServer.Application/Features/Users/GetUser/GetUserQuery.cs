using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Users.GetUser;

[Permission("user:view")]
public sealed record GetUserQuery(Guid Id) : IRequest<Result<UserDto>>;