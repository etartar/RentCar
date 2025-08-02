using RentCarServer.Domain.Branches;
using TS.MediatR;

namespace RentCarServer.Application.Features.Branches.GetAllBranch;

internal sealed class GetAllBranchQueryHandler(IBranchRepository branchRepository) : IRequestHandler<GetAllBranchQuery, IQueryable<BranchDto>>
{
    public Task<IQueryable<BranchDto>> Handle(GetAllBranchQuery request, CancellationToken cancellationToken)
    {
        var response = branchRepository
            .GetAllWithAudit()
            .MapTo();

        return Task.FromResult(response);
    }
}