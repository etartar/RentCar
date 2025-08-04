using GenericRepository;
using RentCarServer.Domain.Branches;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Branches.CreateBranch;

public sealed class CreateBranchCommandHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBranchCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var nameIsExists = await branchRepository.AnyAsync(b => b.Name.Value == request.Name, cancellationToken);

        if (nameIsExists)
        {
            return Result<string>.Failure($"Bu şube adı '{request.Name}' sistemde kayıtlıdır.");
        }

        Name name = new Name(request.Name);

        Branch branch = new Branch(name, request.Address, request.Contact, request.IsActive);

        await branchRepository.AddAsync(branch, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Şube başarılı şekilde kayıt edildi.";
    }
}