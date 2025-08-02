using FluentValidation;

namespace RentCarServer.Application.Features.Auth.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.EmailOrUserName)
            .NotEmpty()
            .WithMessage("Kullanıcı adı veya e-posta boş olamaz.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Şifre boş olamaz.");
    }
}
