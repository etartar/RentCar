using RentCarServer.Domain.Abstractions;

namespace RentCarServer.Application.Features.Users;

public sealed class UserDto : EntityDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public Guid? BranchId { get; set; }
    public string BranchName { get; set; } = default!;
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = default!;
}
