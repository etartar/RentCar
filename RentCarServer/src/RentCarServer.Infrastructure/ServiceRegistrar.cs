using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RentCarServer.Infrastructure.Context;
using RentCarServer.Infrastructure.Options;
using Scrutor;

namespace RentCarServer.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.ConfigureOptions<JwtSetupOptions>();

        services.AddAuthentication().AddJwtBearer();

        services.AddAuthorization();

        services.Configure<MailSettingOptions>(configuration.GetSection("MailSettings"));

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var mailSettings = scope.ServiceProvider.GetRequiredService<IOptions<MailSettingOptions>>();

            if (string.IsNullOrEmpty(mailSettings.Value.UserId))
            {
                services
                    .AddFluentEmail(mailSettings.Value.Email)
                    .AddSmtpSender(mailSettings.Value.Smtp, mailSettings.Value.Port);
            }
            else
            {
                services
                    .AddFluentEmail(mailSettings.Value.Email)
                    .AddSmtpSender(mailSettings.Value.Smtp, mailSettings.Value.Port, mailSettings.Value.UserId, mailSettings.Value.Password);
            }
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("SqlServer")!;

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork>(src => src.GetRequiredService<ApplicationDbContext>());

        services.Scan(action =>
        {
            action.FromAssemblies(typeof(ServiceRegistrar).Assembly)
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(registrationStrategy: RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });

        return services;
    }
}
