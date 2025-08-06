using GenericRepository;
using RentCarServer.Application.Services;
using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Branches;
using RentCarServer.Domain.Roles;
using RentCarServer.Domain.Shared;
using RentCarServer.Domain.Users;
using RentCarServer.Domain.Users.ValueObjects;

namespace RentCarServer.WebAPI;

public static class ExtensionMethods
{
    public static async Task CreateFirstUser(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepostiory>();
        var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
        var branchRepository = scope.ServiceProvider.GetRequiredService<IBranchRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        Branch? branch = await branchRepository.FirstOrDefaultAsync(x => x.Name.Value == "Merkez Şube");
        Role? role = await roleRepository.FirstOrDefaultAsync(x => x.Name.Value == "SysAdmin");

        if (branch is null)
        {
            Name name = new Name("Merkez Şube");
            Address address = new Address("İzmir", "BORNOVA", "Erzene Mah.");
            Contact contact = new Contact("5542249733", "", "emirtartar35@gmail.com");

            branch = new Branch(name, address, contact, true);

            await branchRepository.AddAsync(branch);
        }

        if (role is null)
        {
            role = new Role(new Name("SysAdmin"), true);

            await roleRepository.AddAsync(role);
        }

        if (!await userRepository.AnyAsync(x => x.UserName.Value == "admin"))
        {
            FirstName firstName = new("Emir");
            LastName lastName = new("TARTAR");
            Email email = new("emirtartar35@gmail.com");
            UserName userName = new("admin");
            Password password = new("1");
            IdentityId branchId = branch.Id;
            IdentityId roleId = role.Id;

            var user = new User(firstName, lastName, email, userName, password, branchId, roleId);

            //user.SetCreatedBy();

            await userRepository.AddAsync(user);

            await unitOfWork.SaveChangesAsync();
        }
    }

    public static async Task CleanRemovedPermissionsFromRoleAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var permissionClenaerService = scope.ServiceProvider.GetRequiredService<PermissionClenaerService>();

        await permissionClenaerService.CleanRemovedPermissionsFromRolesAsync();
    }
}
