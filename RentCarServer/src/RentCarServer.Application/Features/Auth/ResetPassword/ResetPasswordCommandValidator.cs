using FluentValidation;

namespace RentCarServer.Application.Features.Auth.ResetPassword;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.ForgotPasswordCode)
            .NotEmpty()
            .WithMessage("Forgot Password Id boş olamaz.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Yeni şifre boş olamaz.")
            .MinimumLength(8)
            .WithMessage("Yeni şifre en az 8 karakter uzunluğunda olmalıdır.");
    }
}
