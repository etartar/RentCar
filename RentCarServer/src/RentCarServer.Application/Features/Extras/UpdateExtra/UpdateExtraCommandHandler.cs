using GenericRepository;
using RentCarServer.Domain.Extras;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Extras.UpdateExtra;

public sealed class UpdateExtraCommandHandler(IExtraRepository extraRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateExtraCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateExtraCommand request, CancellationToken cancellationToken)
    {
        var extra = await extraRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (extra is null)
        {
            return Result<string>.Failure("Ekstra bulunamadı.");
        }

        if (extra.Name.Value != request.Name)
        {
            var nameIsExists = await extraRepository.AnyAsync(b => b.Name.Value == request.Name, cancellationToken);

            if (nameIsExists)
            {
                return Result<string>.Failure("Bu ekstra adı daha önce tanımlanmış.");
            }
        }


        extra.SetName(new Name(request.Name));
        extra.SetPrice(new Price(request.Price));
        extra.SetDescription(new Description(request.Description));
        extra.SetStatus(request.IsActive);

        extraRepository.Update(extra);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ekstra bilgisi başarılı şekilde güncellendi.";
    }
}