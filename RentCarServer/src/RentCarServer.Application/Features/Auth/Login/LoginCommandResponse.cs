namespace RentCarServer.Application.Features.Auth.Login;

public sealed record LoginCommandResponse
{
    public string? Token { get; set; }
    public string? TFACode { get; set; }
}
