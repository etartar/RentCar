using RentCarServer.Application.Features.Permissions.GetAllPermission;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebAPI.Modules;

public static class PermissionModule
{
    public static void MapPermissionEndPoint(this IEndpointRouteBuilder builder)
    {
        var app = builder
            .MapGroup("/permissions")
            .WithTags("Permissions")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        app.MapGet(string.Empty, async (ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetAllPermissionQuery(), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<List<string>>>();
    }
}
