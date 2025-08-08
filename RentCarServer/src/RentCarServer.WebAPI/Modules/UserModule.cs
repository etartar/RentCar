using RentCarServer.Application.Features.Users;
using RentCarServer.Application.Features.Users.CreateUser;
using RentCarServer.Application.Features.Users.DeleteUser;
using RentCarServer.Application.Features.Users.GetUser;
using RentCarServer.Application.Features.Users.UpdateUser;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebAPI.Modules;

public static class UserModule
{
    public static void MapUserEndPoint(this IEndpointRouteBuilder builder)
    {
        var app = builder
            .MapGroup("/users")
            .WithTags("Users")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        app.MapGet("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetUserQuery(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<UserDto>>();

        app.MapPost(string.Empty, async (CreateUserCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapPut(string.Empty, async (UpdateUserCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapDelete("/{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteUserCommand(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();
    }
}
