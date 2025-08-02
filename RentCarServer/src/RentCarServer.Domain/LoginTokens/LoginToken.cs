using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.LoginTokens.ValueObjects;

namespace RentCarServer.Domain.LoginTokens;

public sealed class LoginToken
{
    private LoginToken()
    {
    }

    private LoginToken(IdentityId userId, Token token, ExpiresDate expiresDate)
    {
        Id = new(Guid.CreateVersion7());
        SetUserId(userId);
        SetToken(token);
        SetExpiresDate(expiresDate);
        SetIsActive(new(true));
    }

    #region Properties

    public IdentityId Id { get; private set; } = default!;
    public IdentityId UserId { get; private set; } = default!;
    public Token Token { get; private set; } = default!;
    public ExpiresDate ExpiresDate { get; private set; } = default!;
    public IsActive IsActive { get; private set; } = default!;

    #endregion

    #region Behaviors

    public void SetUserId(IdentityId userId)
    {
        UserId = userId;
    }

    public void SetToken(Token token)
    {
        Token = token;
    }

    public void SetExpiresDate(ExpiresDate expiresDate)
    {
        ExpiresDate = expiresDate;
    }

    public void SetIsActive(IsActive isActive)
    {
        IsActive = isActive;
    }

    #endregion

    #region Factory Methods

    public static LoginToken Create(Guid userId, string token, DateTimeOffset expiresDate)
    {
        IdentityId userIdValue = new IdentityId(userId);
        Token tokenValue = new Token(token);
        ExpiresDate expiresDateValue = new ExpiresDate(expiresDate);

        return new LoginToken(userIdValue, tokenValue, expiresDateValue);
    }

    #endregion
}
