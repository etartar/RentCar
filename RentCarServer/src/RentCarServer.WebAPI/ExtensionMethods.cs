using GenericRepository;
using RentCarServer.Domain.Users;
using RentCarServer.Domain.Users.ValueObjects;

namespace RentCarServer.WebAPI;

public static class ExtensionMethods
{
    public static async Task CreateFirstUser(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepostiory>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        if (!await userRepository.AnyAsync(x => x.UserName.Value == "admin"))
        {
            FirstName firstName = new("Emir");
            LastName lastName = new("TARTAR");
            Email email = new("emirtartar35@gmail.com");
            UserName userName = new("admin");
            Password password = new("1");

            var user = new User(firstName, lastName, email, userName, password);

            //user.SetCreatedBy();

            await userRepository.AddAsync(user);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
