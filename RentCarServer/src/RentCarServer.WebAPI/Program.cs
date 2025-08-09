using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using RentCarServer.Application;
using RentCarServer.Infrastructure;
using RentCarServer.WebAPI;
using RentCarServer.WebAPI.Controllers;
using RentCarServer.WebAPI.Milddlewares;
using RentCarServer.WebAPI.Modules;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddRateLimiter(cfr =>
{
    cfr.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.QueueLimit = 100;
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromSeconds(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    cfr.AddFixedWindowLimiter("login-fixed", opt =>
    {
        opt.QueueLimit = 1;
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    cfr.AddFixedWindowLimiter("forgot-password-fixed", opt =>
    {
        opt.PermitLimit = 3;
        opt.Window = TimeSpan.FromMinutes(5);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    cfr.AddFixedWindowLimiter("check-forgot-password-code-fixed", opt =>
    {
        opt.PermitLimit = 2;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    cfr.AddFixedWindowLimiter("reset-password-fixed", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services
    .AddControllers()
    .AddOData(opt =>
        opt.Select()
            .Filter()
            .Count()
            .Expand()
            .OrderBy()
            .SetMaxTop(null)
            .AddRouteComponents("odata", MainODataController.GetEdmModel())
            );

builder.Services.AddCors();

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
});

builder.Services.AddTransient<CheckTokenMiddleware>();

builder.Services.AddHostedService<CheckLoginTokenBackgroundService>();

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
});

app.UseResponseCompression();

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler();

app.UseMiddleware<CheckTokenMiddleware>();

app.UseRateLimiter();

app.MapControllers()
    .RequireRateLimiting("fixed")
    .RequireAuthorization();

app.MapAuthEndPoint();

app.MapBranchEndPoint();

app.MapRoleEndPoint();

app.MapPermissionEndPoint();

app.MapUserEndPoint();

app.MapCategoryEndPoint();

app.MapProtectionPackageEndPoint();

app.MapExtraEndPoint();

//await app.CreateFirstUser();

await app.CleanRemovedPermissionsFromRoleAsync();

app.Run();
