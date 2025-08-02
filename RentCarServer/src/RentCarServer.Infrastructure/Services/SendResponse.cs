
namespace RentCarServer.Infrastructure.Services;

internal class SendResponse
{
    public static implicit operator SendResponse(FluentEmail.Core.Models.SendResponse v)
    {
        throw new NotImplementedException();
    }
}