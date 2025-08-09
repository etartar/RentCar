using FluentValidation;

namespace RentCarServer.Application.Features.Categories.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Geçerli bir kategori adı giriniz.");
    }
}
