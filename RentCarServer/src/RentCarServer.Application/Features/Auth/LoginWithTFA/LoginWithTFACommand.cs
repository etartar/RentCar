using GenericRepository;
using RentCarServer.Application.Features.Auth.Login;
using RentCarServer.Application.Services;
using RentCarServer.Domain.Users;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Auth.LoginWithTFA;

public sealed record LoginWithTFACommand(
    string EmailOrUserName,
    string TFACode,
    string TFAConfirmCode) : IRequest<Result<LoginCommandResponse>>;


internal sealed class LoginWithTFACommandHandler(
    IUserRepostiory userRepostiory,
    IJwtProvider jwtProvider,
    IUnitOfWork unitOfWork) : IRequestHandler<LoginWithTFACommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginWithTFACommand request, CancellationToken cancellationToken)
    {
        User user = await userRepostiory.FirstOrDefaultAsync(x => x.Email.Value == request.EmailOrUserName || x.UserName.Value == request.EmailOrUserName);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı adı ve/veya şifre yanlış.");
        }

        if (user.TFAIsCompleted is null ||
            user.TFAExpiresDate is null ||
            user.TFACode is null ||
            user.TFAConfirmCode is null ||
            user.TFAIsCompleted.Value ||
            user.TFAExpiresDate.Value < DateTimeOffset.Now ||
            user.TFAConfirmCode.Value != request.TFAConfirmCode ||
            user.TFACode.Value != request.TFACode)
        {
            return Result<LoginCommandResponse>.Failure("TFA kodu geçersiz.");
        }

        user.SetTFACompleted();

        userRepostiory.Update(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var token = await jwtProvider.CreateTokenAsync(user, cancellationToken);

        return new LoginCommandResponse
        {
            Token = token
        };
    }
}
