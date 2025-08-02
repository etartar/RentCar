using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Auth.Login;

//public sealed record LoginCommand(string FirstName, string LastName, string Email, string UserName, string Password) : IRequest<Result<string>>;
public sealed record LoginCommand(string EmailOrUserName, string Password) : IRequest<Result<LoginCommandResponse>>;
