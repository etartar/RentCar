using FluentValidation;

namespace RentCarServer.Application.Features.Branches.UpdateBranch;

public sealed class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name).NotEmpty().WithMessage("Geçerli bir şube adı giriniz.");

        RuleFor(x => x.Address.City).NotEmpty().WithMessage("Geçerli bir şehir seçiniz.");

        RuleFor(x => x.Address.District).NotEmpty().WithMessage("Geçerli bir ilçe seçiniz.");

        RuleFor(x => x.Address.FullAddress).NotEmpty().WithMessage("Geçerli bir tam adres giriniz.");

        RuleFor(x => x.Address.PhoneNumber1).NotEmpty().WithMessage("Geçerli bir telefon numarası giriniz.");
    }
}