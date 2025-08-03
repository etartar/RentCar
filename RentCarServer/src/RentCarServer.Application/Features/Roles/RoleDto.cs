using RentCarServer.Domain.Abstractions;

namespace RentCarServer.Application.Features.Roles;

public sealed class RoleDto : EntityDto
{
    public string Name { get; set; } = default!;
    public int PermissionCount { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();
}
