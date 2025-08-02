namespace RentCarServer.Application.Attributes;

public sealed class PermissionAttribute : Attribute
{
    public string? Permission { get; }

    public PermissionAttribute()
    {
    }

    public PermissionAttribute(string permission)
    {
        Permission = permission;
    }
}
