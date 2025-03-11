using MediatR;

namespace Auth.Application.User.CreateUser;

public class CreateUserCommand : IRequest<CreateUserResult>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}