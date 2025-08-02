using GenericRepository;
using RentCarServer.Application.Services;
using RentCarServer.Domain.Users;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Auth.Login;

internal sealed class LoginCommandHandler(
    IUserRepostiory userRepostiory,
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider,
    IMailService mailService) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User user = await userRepostiory.FirstOrDefaultAsync(x => x.Email.Value == request.EmailOrUserName || x.UserName.Value == request.EmailOrUserName);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı adı ve/veya şifre yanlış.");
        }

        bool verifyPassword = user.VerifyPasswordHash(request.Password);

        if (!verifyPassword)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı adı ve/veya şifre yanlış.");
        }

        if (!user.TFAStatus.Value)
        {
            var token = await jwtProvider.CreateTokenAsync(user, cancellationToken);

            return new LoginCommandResponse
            {
                Token = token
            };
        }

        user.CreateTFACode();

        userRepostiory.Update(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        string to = user.Email.Value;
        string subject = "Giriş Onayı";
        string body = @$"Uygulamaya girmek için aşağıda ki kodu kullanabilirsiniz. Bu kod sadece 5 dakika geçerlidir. <p><h4>${user.TFAConfirmCode?.Value}</h4></p>";

        await mailService.SendAsync(to, subject, body, cancellationToken);

        return new LoginCommandResponse
        {
            TFACode = user.TFACode!.Value
        };
    }
}