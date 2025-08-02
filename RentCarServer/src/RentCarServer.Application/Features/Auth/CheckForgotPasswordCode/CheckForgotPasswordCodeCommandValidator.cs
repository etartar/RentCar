using FluentValidation;

namespace RentCarServer.Application.Features.Auth.CheckForgotPasswordCode;

public sealed class CheckForgotPasswordCodeCommandValidator : AbstractValidator<CheckForgotPasswordCodeCommand>
{
    public CheckForgotPasswordCodeCommandValidator()
    {
        RuleFor(x => x.ForgotPasswordCode)
            .NotEmpty().WithMessage("Şifre sıfırlama kodu boş olamaz.");
    }
}