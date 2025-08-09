using RentCarServer.Application.Features.ProtectionPackages;
using RentCarServer.Application.Features.ProtectionPackages.CreateProtectionPackage;
using RentCarServer.Application.Features.ProtectionPackages.DeleteProtectionPackage;
using RentCarServer.Application.Features.ProtectionPackages.GetProtectionPackage;
using RentCarServer.Application.Features.ProtectionPackages.UpdateProtectionPackage;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebAPI.Modules;

public static class ProtectionPackageModule
{
    public static void MapProtectionPackageEndPoint(this IEndpointRouteBuilder builder)
    {
        var app = builder
            .MapGroup("/protection-packages")
            .WithTags("ProtectionPackages")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        app.MapGet("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetProtectionPackageQuery(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<ProtectionPackageDto>>();

        app.MapPost(string.Empty, async (CreateProtectionPackageCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapPut(string.Empty, async (UpdateProtectionPackageCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapDelete("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteProtectionPackageCommand(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();
    }
}
