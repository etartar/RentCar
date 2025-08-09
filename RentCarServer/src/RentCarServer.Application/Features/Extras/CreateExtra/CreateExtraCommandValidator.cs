using FluentValidation;

namespace RentCarServer.Application.Features.Extras.CreateExtra;

public sealed class CreateExtraCommandValidator : AbstractValidator<CreateExtraCommand>
{
    public CreateExtraCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Geçerli bir ekstra adı giriniz.");

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
