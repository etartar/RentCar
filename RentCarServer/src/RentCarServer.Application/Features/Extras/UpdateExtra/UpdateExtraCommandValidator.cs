using FluentValidation;

namespace RentCarServer.Application.Features.Extras.UpdateExtra;

public sealed class UpdateExtraCommandValidator : AbstractValidator<UpdateExtraCommand>
{
    public UpdateExtraCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Geçerli bir ekstra adı giriniz.");

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
