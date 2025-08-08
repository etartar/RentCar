using RentCarServer.Application.Attributes;
using TS.MediatR;

namespace RentCarServer.Application.Features.Users.GetAllUser;

[Permission("user:view")]
public sealed record GetAllUserQuery() : IRequest<IQueryable<UserDto>>;