using GenericRepository;
using RentCarServer.Domain.Extras;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Extras.DeleteExtra;

internal sealed class DeleteExtraCommandHandler(IExtraRepository extraRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteExtraCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteExtraCommand request, CancellationToken cancellationToken)
    {
        var extra = await extraRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (extra is null)
        {
            return Result<string>.Failure("Ekstra bulunamadı.");
        }

        extra.Delete();

        extraRepository.Update(extra);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ekstra başarılı bir şekilde silindi.";
    }
}