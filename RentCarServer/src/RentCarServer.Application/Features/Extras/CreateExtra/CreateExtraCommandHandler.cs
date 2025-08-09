using GenericRepository;
using RentCarServer.Domain.Extras;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Extras.CreateExtra;

public sealed class CreateExtraCommandHandler(IExtraRepository extraRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateExtraCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateExtraCommand request, CancellationToken cancellationToken)
    {
        var nameIsExists = await extraRepository.AnyAsync(b => b.Name.Value == request.Name, cancellationToken);

        if (nameIsExists)
        {
            return Result<string>.Failure($"Bu ekstra adı '{request.Name}' sistemde kayıtlıdır.");
        }

        Name name = new Name(request.Name);
        Price price = new Price(request.Price);
        Description description = new Description(request.Description);

        var extra = new Extra(name, price, description, request.IsActive);

        await extraRepository.AddAsync(extra, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ekstra başarılı şekilde kayıt edildi.";
    }
}