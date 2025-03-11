using MediatR;

namespace Auth.Application.User.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    public Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}