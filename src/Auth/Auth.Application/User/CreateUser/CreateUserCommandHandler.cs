﻿using Auth.Application.Interfaces;
using Auth.ORM.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Common.Exceptions;

namespace Auth.Application.User.CreateUser;

public class CreateUserCommandHandler(
    AuthDbContext context,
    IPasswordHasher passwordHasher,
    IValidator<CreateUserCommand> validator) : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser =
            await context.Users.SingleOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (existingUser is not null)
            throw new BadRequestException(
                $"O nome de usuário '{request.Username}' já existe.\n Por favor informe um nome diferente.");

        var user = request.Adapt<Domain.Entities.User>();
        user.Password = passwordHasher.HashPassword(user.Password!);

        await context.Users.AddAsync(user, cancellationToken);

        var success = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!success)
            throw new BadRequestException("Houve uma falha ao inserir o usuário.");

        return user.Adapt<CreateUserResult>();
    }
}