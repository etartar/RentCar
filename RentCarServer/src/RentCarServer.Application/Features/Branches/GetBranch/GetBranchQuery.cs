using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Branches.GetBranch;

[Permission("branch:view")]
public sealed record GetBranchQuery(Guid Id) : IRequest<Result<BranchDto>>;
