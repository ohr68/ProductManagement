using Auth.Application.Interfaces;
using Auth.Jwt.Interfaces;
using Auth.ORM.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Common.Exceptions;

namespace Auth.Application.Auth;

public class AuthCommandHandler(
    AuthDbContext context,
    IJwtService jwtService,
    IPasswordHasher passwordHasher,
    IValidator<AuthCommand> authValidator) : IRequestHandler<AuthCommand, AuthResult>
{
    public async Task<AuthResult> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await authValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await context.Users.SingleOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (user is null)
            throw new NotFoundException($"Usuário '{request.Username}' não encontrado.");

        var validPassword = passwordHasher.VerifyPassword(user.Password!, request.Password!);

        if (!validPassword)
            throw new BadRequestException("Senha inválida.");

        var token = jwtService.GenerateToken(request.Username!);

        return new AuthResult(token);
    }
}