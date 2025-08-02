using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Auth.ResetPassword;

public sealed record ResetPasswordCommand(Guid ForgotPasswordCode, string NewPassword, bool LogoutAllDevices) : IRequest<Result<string>>;
