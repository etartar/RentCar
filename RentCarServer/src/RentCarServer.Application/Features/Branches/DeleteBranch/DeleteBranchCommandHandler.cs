using GenericRepository;
using RentCarServer.Domain.Branches;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Branches.DeleteBranch;

internal sealed class DeleteBranchCommandHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteBranchCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await branchRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (branch is null)
        {
            return Result<string>.Failure("Şube bulunamadı.");
        }

        branch.Delete();

        branchRepository.Update(branch);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Şube başarılı bir şekilde silindi.";
    }
}
