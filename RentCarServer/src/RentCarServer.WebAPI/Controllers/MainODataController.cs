using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using RentCarServer.Application.Features.Branches;
using RentCarServer.Application.Features.Branches.GetAllBranch;
using RentCarServer.Application.Features.Categories;
using RentCarServer.Application.Features.Categories.GetAllCategory;
using RentCarServer.Application.Features.ProtectionPackages;
using RentCarServer.Application.Features.ProtectionPackages.GetAllProtectionPackage;
using RentCarServer.Application.Features.Roles;
using RentCarServer.Application.Features.Roles.GetAllRole;
using RentCarServer.Application.Features.Users;
using RentCarServer.Application.Features.Users.GetAllUser;
using TS.MediatR;

namespace RentCarServer.WebAPI.Controllers;

[Route("odata")]
[ApiController]
[EnableQuery]
public class MainODataController : ODataController
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new();

        builder.EnableLowerCamelCase();
        builder.EntitySet<BranchDto>("branches");
        builder.EntitySet<RoleDto>("roles");
        builder.EntitySet<UserDto>("users");
        builder.EntitySet<CategoryDto>("categories");
        builder.EntitySet<ProtectionPackageDto>("protection-packages");
        return builder.GetEdmModel();
    }

    [HttpGet("branches")]
    public IQueryable<BranchDto> Branches(ISender sender, CancellationToken cancellationToken = default)
        => sender.Send(new GetAllBranchQuery(), cancellationToken).Result;

    [HttpGet("roles")]
    public IQueryable<RoleDto> Roles(ISender sender, CancellationToken cancellationToken = default)
        => sender.Send(new GetAllRoleQuery(), cancellationToken).Result;

    [HttpGet("users")]
    public IQueryable<UserDto> Users(ISender sender, CancellationToken cancellationToken = default)
        => sender.Send(new GetAllUserQuery(), cancellationToken).Result;

    [HttpGet("categories")]
    public IQueryable<CategoryDto> Categories(ISender sender, CancellationToken cancellationToken = default)
        => sender.Send(new GetAllCategoryQuery(), cancellationToken).Result;

    [HttpGet("protection-packages")]
    public IQueryable<ProtectionPackageDto> ProtectionPackages(ISender sender, CancellationToken cancellationToken = default)
        => sender.Send(new GetAllProtectionPackageQuery(), cancellationToken).Result;
}
