using RentCarServer.Domain.Abstractions;

namespace RentCarServer.Application.Features.Roles;

public sealed class RoleDto : EntityDto
{
    public string Name { get; set; } = default!;
}
