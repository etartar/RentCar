using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Auth.CheckForgotPasswordCode;

public sealed record CheckForgotPasswordCodeCommand(Guid ForgotPasswordCode) : IRequest<Result<bool>>;
