using MediatR;

namespace Auth.Application.Auth;

public class AuthCommand : IRequest<AuthResult>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}