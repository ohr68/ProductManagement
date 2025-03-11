﻿using Auth.Application.CustomValidators;
using FluentValidation;

namespace Auth.Application.User.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do usuário.")
            .MaximumLength(100)
            .WithMessage("O tamanho máximo permitido para o nome de usuário é de 100 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("É obrigatório informar o e-mail.")
            .EmailAddress()
            .WithMessage("Informe um e-mail válido.")
            .MaximumLength(150)
            .WithMessage("O tamanho máximo permitido para o e-mail é de 150 caracteres.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("É obrigatório informar a senha.")
            .SetValidator(new StrongPasswordValidator<CreateUserCommand>()!);
    }
}