using RentCarServer.Application.Features.Categories;
using RentCarServer.Application.Features.Categories.CreateCategory;
using RentCarServer.Application.Features.Categories.DeleteCategory;
using RentCarServer.Application.Features.Categories.GetCategory;
using RentCarServer.Application.Features.Categories.UpdateCategory;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.WebAPI.Modules;

public static class CategoryModule
{
    public static void MapCategoryEndPoint(this IEndpointRouteBuilder builder)
    {
        var app = builder
            .MapGroup("/categories")
            .WithTags("Categories")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        app.MapGet("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetCategoryQuery(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<CategoryDto>>();

        app.MapPost(string.Empty, async (CreateCategoryCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapPut(string.Empty, async (UpdateCategoryCommand request, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();

        app.MapDelete("{id:guid}", async (Guid id, ISender mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

            return result.IsSuccessful
                ? Results.Ok(result)
                : Results.BadRequest(result);
        })
            .Produces<Result<string>>();
    }
}
