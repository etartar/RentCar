using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Users.ValueObjects;

namespace RentCarServer.Domain.Users;

public sealed class User : Entity
{
    private User() : base()
    {
    }

    public User(
        FirstName firstName,
        LastName lastName,
        Email email,
        UserName userName,
        Password password) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = new FullName(firstName.Value + " " + lastName.Value + " (" + email.Value + ")");
        Email = email;
        UserName = userName;
        SetPassword(password);
        IsForgotPasswordCompleted = new(true);
        SetTFAStatus(new(false));
    }

    public FirstName FirstName { get; private set; } = default!;
    public LastName LastName { get; private set; } = default!;
    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public UserName UserName { get; private set; } = default!;
    public Password Password { get; private set; } = default!;
    public ForgotPasswordCode? ForgotPasswordCode { get; private set; }
    public ForgotPasswordCreatedDate? ForgotPasswordCreatedDate { get; private set; }
    public IsForgotPasswordCompleted IsForgotPasswordCompleted { get; private set; } = default!;
    public TFAStatus TFAStatus { get; private set; } = default!;
    public TFACode? TFACode { get; private set; } = default!;
    public TFAConfirmCode? TFAConfirmCode { get; private set; } = default!;
    public TFAExpiresDate? TFAExpiresDate { get; private set; } = default!;
    public TFAIsCompleted? TFAIsCompleted { get; private set; } = default!;

    #region Behaviors

    public bool VerifyPasswordHash(string password)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(Password.PasswordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(Password.PasswordHash);
    }

    public void CreateForgotPasswordCode()
    {
        ForgotPasswordCode = new ForgotPasswordCode(Guid.CreateVersion7());
        ForgotPasswordCreatedDate = new ForgotPasswordCreatedDate(DateTimeOffset.UtcNow);
        IsForgotPasswordCompleted = new IsForgotPasswordCompleted(false);
    }

    public void SetPassword(Password newPassword)
    {
        Password = newPassword;
    }

    public void SetTFAStatus(TFAStatus status)
    {
        TFAStatus = status;
    }

    public void SetTFACompleted()
    {
        TFAIsCompleted = new(true);
    }

    public void CreateTFACode()
    {
        var code = Guid.CreateVersion7().ToString();
        var confirmCode = Guid.CreateVersion7().ToString();
        var expires = DateTimeOffset.UtcNow.AddMinutes(5);

        TFACode = new(code);
        TFAConfirmCode = new(confirmCode);
        TFAExpiresDate = new(expires);
        TFAIsCompleted = new(false);
    }

    #endregion
}
