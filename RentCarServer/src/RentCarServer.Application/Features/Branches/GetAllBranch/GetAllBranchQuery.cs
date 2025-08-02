using RentCarServer.Application.Attributes;
using TS.MediatR;

namespace RentCarServer.Application.Features.Branches.GetAllBranch;

[Permission("branch:view")]
public sealed record GetAllBranchQuery() : IRequest<IQueryable<BranchDto>>;
