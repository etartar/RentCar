using FluentValidation;

namespace RentCarServer.Application.Features.ProtectionPackages.UpdateProtectionPackage;

public sealed class UpdateProtectionPackageCommandValidator : AbstractValidator<UpdateProtectionPackageCommand>
{
    public UpdateProtectionPackageCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Geçerli bir paket güvence adı giriniz.");
    }
}
