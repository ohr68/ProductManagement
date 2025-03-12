using FluentValidation;

namespace Auth.Application.Auth;

public class AuthValidator : AbstractValidator<AuthCommand>
{
    public AuthValidator()
    {
        RuleFor(a => a.Username)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do usuário.");

        RuleFor(a => a.Password)
            .NotEmpty()
            .WithMessage("É obrigatório informar a senha.");
    }
}