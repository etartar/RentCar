using RentCarServer.Application.Features.Extras;
using RentCarServer.Application.Features.Extras.CreateExtra;
using RentCarServer.Application.Features.Extras.DeleteExtra;
using RentCarServer.Application.Features.Extras.GetExtra;
using RentCarServer.Application.Features.Extras.UpdateExtra;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebAPI.Modules;

public static class ExtraModule
{
    public static void MapExtraEndPoint(this IEndpointRouteBuilder builder)
    {
        var app = builder
            .MapGroup("/extras")
            .WithTags("Extras")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        app.MapGet("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetExtraQuery(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<ExtraDto>>();

        app.MapPost(string.Empty, async (CreateExtraCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapPut(string.Empty, async (UpdateExtraCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapDelete("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteExtraCommand(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();
    }
}
