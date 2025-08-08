using FluentValidation;

namespace RentCarServer.Application.Features.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Geçerli bir ad giriniz.");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Geçerli bir soyad giriniz.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Geçerli bir e-posta giriniz.");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Geçerli bir kullanıcı adı giriniz.");
    }
}