using Microsoft.EntityFrameworkCore;
using RentCarServer.Domain.Branches;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Branches.GetBranch;

internal sealed class GetBranchQueryHandler(IBranchRepository branchRepository) : IRequestHandler<GetBranchQuery, Result<BranchDto>>
{
    public async Task<Result<BranchDto>> Handle(GetBranchQuery request, CancellationToken cancellationToken)
    {
        var branch = await branchRepository
            .GetAllWithAudit()
            .MapTo()
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (branch is null)
        {
            return Result<BranchDto>.Failure("Şube bulunamadı.");
        }

        return branch;
    }
}
