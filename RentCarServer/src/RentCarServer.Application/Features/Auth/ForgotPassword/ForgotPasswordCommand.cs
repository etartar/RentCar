using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Auth.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : IRequest<Result<string>>;
