
using RentCarServer.Domain.LoginTokens;
using System.Security.Claims;

namespace RentCarServer.WebAPI.Milddlewares;

public class CheckTokenMiddleware(ILoginTokenRepository loginTokenRepository) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        if (string.IsNullOrWhiteSpace(token))
        {
            await next(context);
            return;
        }

        var userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new AppTokenException();
        }

        var isTokenAvailable = await loginTokenRepository.AnyAsync(x =>
            x.UserId == userId &&
            x.Token.Value == token &&
            x.IsActive.Value == true);

        if (!isTokenAvailable)
        {
            throw new AppTokenException();
        }

        await next(context);
    }
}

public sealed class AppTokenException : Exception
{
}