using RentCarServer.Application.Attributes;
using RentCarServer.Domain.Shared;
using TS.MediatR;
using TS.Result;

namespace RentCarServer.Application.Features.Branches.UpdateBranch;

[Permission("branch:edit")]
public sealed record UpdateBranchCommand(Guid Id, string Name, Address Address, Contact Contact, bool IsActive) : IRequest<Result<string>>;
