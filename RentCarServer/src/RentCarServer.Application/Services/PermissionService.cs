using RentCarServer.Application.Attributes;
using System.Reflection;

namespace RentCarServer.Application.Services;

public sealed class PermissionService
{
    public List<string> GetAll()
    {
        var permissions = new HashSet<string>();

        permissions.Add("dashboard:view");

        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            var permissionAttribute = type.GetCustomAttribute<PermissionAttribute>();

            if (permissionAttribute is not null && !string.IsNullOrEmpty(permissionAttribute.Permission))
            {
                permissions.Add(permissionAttribute.Permission);
            }
        }

        return permissions.ToList();
    }
}
