using Microsoft.AspNetCore.Http;
using RentCarServer.Application.Services;
using System.Security.Claims;

namespace RentCarServer.Infrastructure.Services;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid? GetUserId()
    {
        string? userId = httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)
            ?.Value;

        if (userId is null)
        {
            //throw new ArgumentNullException("Kullanıcı bilgisi bulunamadı.");
            return null;
        }

        try
        {
            return Guid.Parse(userId);
        }
        catch (Exception ex)
        {
            throw new FormatException("Kullanıcı ID'si geçersiz formatta.", ex);
        }
    }

    public Guid GetBranchId()
    {
        string? branchId = httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(p => p.Type == "branchId")
            ?.Value;

        if (branchId is null)
        {
            throw new ArgumentNullException("Şube bilgisi bulunamadı.");
        }

        try
        {
            return Guid.Parse(branchId);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Şube ID'si geçersiz formatta.", ex);
        }
    }

    public string GetRoleName()
    {
        string? roleName = httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(p => p.Type == ClaimTypes.Role)
            ?.Value;

        if (roleName is null)
        {
            throw new ArgumentNullException("Rol bilgisi bulunamadı.");
        }

        return roleName;
    }
}
