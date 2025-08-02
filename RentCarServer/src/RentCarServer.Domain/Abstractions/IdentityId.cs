namespace RentCarServer.Domain.Abstractions;

public sealed record IdentityId(Guid Value)
{
    public static implicit operator Guid(IdentityId id) => id.Value;

    public static implicit operator string(IdentityId id) => id.ToString();

    public override string ToString() => Value.ToString();
}