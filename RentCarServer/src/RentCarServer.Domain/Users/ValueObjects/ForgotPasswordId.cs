namespace RentCarServer.Domain.Users.ValueObjects;

public sealed record ForgotPasswordCode(Guid Value)
{
    public static implicit operator Guid(ForgotPasswordCode forgotPasswordId) => forgotPasswordId.Value;

    public static implicit operator string(ForgotPasswordCode forgotPasswordId) => forgotPasswordId.ToString();
}
