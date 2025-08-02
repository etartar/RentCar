using RentCarServer.Application.Features.Roles;
using RentCarServer.Application.Features.Roles.CreateRole;
using RentCarServer.Application.Features.Roles.DeleteRole;
using RentCarServer.Application.Features.Roles.GetRole;
using RentCarServer.Application.Features.Roles.UpdateRole;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebAPI.Modules;

public static class RoleModule
{
    public static void MapRoleEndPoint(this IEndpointRouteBuilder builder)
    {
        var app = builder
            .MapGroup("/roles")
            .WithTags("Roles")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        app.MapGet("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetRoleQuery(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<RoleDto>>();

        app.MapPost(string.Empty, async (CreateRoleCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapPut(string.Empty, async (UpdateRoleCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapDelete("/{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteRoleCommand(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();
    }
}
