using FluentValidation;

namespace RentCarServer.Application.Features.ProtectionPackages.CreateProtectionPackage;

public sealed class CreateProtectionPackageCommandValidator : AbstractValidator<CreateProtectionPackageCommand>
{
    public CreateProtectionPackageCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Geçerli bir paket güvence adı giriniz.");
    }
}
