using FluentValidation;

namespace RentCarServer.Application.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Geçerli bir mail adresi giriniz.")
            .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.");
    }
}
