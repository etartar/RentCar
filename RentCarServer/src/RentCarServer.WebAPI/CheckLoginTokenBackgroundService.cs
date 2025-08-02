
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.LoginTokens;

namespace RentCarServer.WebAPI;

public sealed class CheckLoginTokenBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var loginTokenRepository = scope.ServiceProvider.GetRequiredService<ILoginTokenRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var activeLoginTokens = await loginTokenRepository
            .Where(x => x.IsActive.Value == true && x.ExpiresDate.Value < DateTimeOffset.Now)
            .ToListAsync(stoppingToken);

        foreach (var token in activeLoginTokens)
        {
            token.SetIsActive(new IsActive(false));
        }

        loginTokenRepository.UpdateRange(activeLoginTokens);

        await unitOfWork.SaveChangesAsync(stoppingToken);

        await Task.Delay(TimeSpan.FromDays(1));
    }
}
