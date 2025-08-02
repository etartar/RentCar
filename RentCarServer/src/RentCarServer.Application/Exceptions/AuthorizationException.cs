namespace RentCarServer.Application.Exceptions;

public sealed class AuthorizationException : Exception
{
    public AuthorizationException() : base("Yetkiniz bulunmamaktadır.")
    {
    }

    public AuthorizationException(string message) : base(message)
    {
    }
}