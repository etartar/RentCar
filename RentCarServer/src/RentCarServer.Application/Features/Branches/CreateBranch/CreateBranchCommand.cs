using RentCarServer.Application.Attributes;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Branches.CreateBranch;

[Permission("branch:create")]
public sealed record CreateBranchCommand(string Name, Address Address, Contact Contact, bool IsActive) : IRequest<Result<string>>;
