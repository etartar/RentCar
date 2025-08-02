using RentCarServer.Application.Attributes;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Branches.DeleteBranch;

[Permission("branch:delete")]
public sealed record DeleteBranchCommand(Guid Id) : IRequest<Result<string>>;
