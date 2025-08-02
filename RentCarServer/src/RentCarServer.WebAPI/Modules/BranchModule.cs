using RentCarServer.Application.Features.Branches;
using RentCarServer.Application.Features.Branches.CreateBranch;
using RentCarServer.Application.Features.Branches.DeleteBranch;
using RentCarServer.Application.Features.Branches.GetBranch;
using RentCarServer.Application.Features.Branches.UpdateBranch;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebAPI.Modules;

public static class BranchModule
{
    public static void MapBranchEndPoint(this IEndpointRouteBuilder builder)
    {
        var app = builder
            .MapGroup("/branches")
            .WithTags("Branches")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        app.MapGet("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetBranchQuery(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<BranchDto>>();

        app.MapPost(string.Empty, async (CreateBranchCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapPut(string.Empty, async (UpdateBranchCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapDelete("/{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteBranchCommand(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();
    }
}
