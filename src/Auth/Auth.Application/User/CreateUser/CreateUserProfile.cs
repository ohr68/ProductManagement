using Mapster;
namespace Auth.Application.User.CreateUser;

public class CreateUserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserCommand, Domain.Entities.User>();
        config.NewConfig<Domain.Entities.User, CreateUserResult>();
    }
}