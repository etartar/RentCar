using RentCarServer.Application.Features.Auth.CheckForgotPasswordCode;
using RentCarServer.Application.Features.Auth.ForgotPassword;
using RentCarServer.Application.Features.Auth.Login;
using RentCarServer.Application.Features.Auth.LoginWithTFA;
using RentCarServer.Application.Features.Auth.ResetPassword;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebAPI.Modules;

public static class AuthModule
{
    public static void MapAuthEndPoint(this IEndpointRouteBuilder builder)
    {
        var app = builder
            .MapGroup("/auth")
            .WithTags("Auth");

        app.MapPost("/login", async (ISender mediator, LoginCommand request, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.InternalServerError(result);
        })
            .Produces<Result<LoginCommandResponse>>()
            .RequireRateLimiting("login-fixed");

        app.MapPost("/login-with-tfa", async (ISender mediator, LoginWithTFACommand request, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.InternalServerError(result);
        })
            .Produces<Result<LoginCommandResponse>>()
            .RequireRateLimiting("login-fixed");

        app.MapPost("/forgot-password/{email}", async (string email, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new ForgotPasswordCommand(email), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.InternalServerError(result);
        })
            .Produces<Result<string>>()
            .RequireRateLimiting("forgot-password-fixed");

        app.MapPost("/reset-password", async (ISender mediator, ResetPasswordCommand request, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.InternalServerError(result);
        })
            .Produces<Result<string>>()
            .RequireRateLimiting("reset-password-fixed");

        app.MapGet("/check-forgot-password-code/{forgotPasswordCode}", async (Guid forgotPasswordCode, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new CheckForgotPasswordCodeCommand(forgotPasswordCode), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.InternalServerError(result);
        })
            .Produces<Result<string>>()
            .RequireRateLimiting("check-forgot-password-code-fixed");
    }
}
