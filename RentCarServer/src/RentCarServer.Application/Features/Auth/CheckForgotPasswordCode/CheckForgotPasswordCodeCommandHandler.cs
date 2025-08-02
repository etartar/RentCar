using RentCarServer.Domain.Users;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Auth.CheckForgotPasswordCode;

internal sealed class CheckForgotPasswordCodeCommandHandler(IUserRepostiory userRepostiory) : IRequestHandler<CheckForgotPasswordCodeCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CheckForgotPasswordCodeCommand request, CancellationToken cancellationToken)
    {
        User user = await userRepostiory.FirstOrDefaultAsync(
                x => x.ForgotPasswordCode != null &&
                x.ForgotPasswordCode.Value == request.ForgotPasswordCode
                && x.IsForgotPasswordCompleted.Value == false);

        if (user is null)
        {
            return Result<bool>.Failure("Şifre sıfırlama değeriniz geçersiz.");
        }

        var fpDate = user.ForgotPasswordCreatedDate!.Value.AddDays(1);
        var now = DateTimeOffset.Now;

        if (fpDate < now)
        {
            return Result<bool>.Failure("Şifre sıfırlama değeriniz geçersiz.");
        }

        return true;
    }
}