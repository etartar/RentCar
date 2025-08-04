using GenericRepository;
using RentCarServer.Domain.Branches;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Branches.UpdateBranch;

public sealed class UpdateBranchCommandHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateBranchCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await branchRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (branch is null)
        {
            return Result<string>.Failure("Şube bulunamadı.");
        }

        Name name = new Name(request.Name);

        branch.SetName(name);
        branch.SetAddress(request.Address);
        branch.SetContact(request.Contact);
        branch.SetStatus(request.IsActive);

        branchRepository.Update(branch);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Şube bilgisi başarılı şekilde güncellendi.";
    }
}
