using GenericRepository;
using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.LoginTokens;
using RentCarServer.Domain.Users;
using RentCarServer.Domain.Users.ValueObjects;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Auth.ResetPassword;

internal sealed class ResetPasswordCommandHandler(
    IUserRepostiory userRepostiory,
    ILoginTokenRepository loginTokenRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ResetPasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        User user = await userRepostiory.FirstOrDefaultAsync(
                x => x.ForgotPasswordCode != null &&
                x.ForgotPasswordCode.Value == request.ForgotPasswordCode
                && x.IsForgotPasswordCompleted.Value == false);

        if (user is null)
        {
            return Result<string>.Failure("Şifre sıfırlama değeriniz geçersiz.");
        }

        var fpDate = user.ForgotPasswordCreatedDate!.Value.AddDays(1);
        var now = DateTimeOffset.Now;

        if (fpDate < now)
        {
            return Result<string>.Failure("Şifre sıfırlama değeriniz geçersiz.");
        }

        Password newPassword = new(request.NewPassword);

        user.SetPassword(newPassword);

        userRepostiory.Update(user);

        if (request.LogoutAllDevices)
        {
            var loginTokens = await loginTokenRepository
                .Where(x => x.UserId == user.Id && x.IsActive.Value == true)
                .ToListAsync(cancellationToken);

            foreach (var loginToken in loginTokens)
            {
                loginToken.SetIsActive(new(false));
            }

            loginTokenRepository.UpdateRange(loginTokens);
        }

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Succeed("Şifreniz başarıyla sıfırlandı. Yeni şifrenizle giriş yapabilirsiniz.");
    }
}
