using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Shared;

namespace RentCarServer.Application.Features.Branches;

public sealed class BranchDto : EntityDto
{
    public string Name { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public Contact Contact { get; set; } = default!;
}