namespace RentCarServer.Application.Services;

public interface IUserContext
{
    Guid? GetUserId();
    Guid GetBranchId();
    string GetRoleName();
}
