namespace Auth.Application.Auth;

public record AuthResult(string? AccessToken, UserResult User);

public record UserResult(string? UserName, string? Email);
