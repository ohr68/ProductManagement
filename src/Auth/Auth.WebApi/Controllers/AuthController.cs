using Auth.Application.Auth;
using Auth.Application.User.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Common.WebApi;

namespace Auth.WebApi.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas à login e registro de usuário
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : BaseController
{
    
    /// <summary>
    /// Efetua o login de um usuário
    /// </summary>
    /// <param name="command">Modelo da requisição contendo os dados para login</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponseWithData<AuthResult>), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, contentType: "application/json")]
    public async Task<IActionResult> Login([FromBody] AuthCommand command,
        CancellationToken cancellationToken = default)
        => Ok(new ApiResponseWithData<AuthResult>(await mediator.Send(command, cancellationToken),
            true,
            "Login efetuado com sucesso."));
    
    /// <summary>
    /// Cadastra um novo usuário na base
    /// </summary>
    /// <param name="command">Modelo da requisição de criação de um novo usuário</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResult>), StatusCodes.Status201Created, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, contentType: "application/json")]
    public async Task<IActionResult> Registrar([FromBody] CreateUserCommand command, CancellationToken cancellationToken = default)
      => Created(string.Empty, new ApiResponseWithData<CreateUserResult>(await mediator.Send(command, cancellationToken),
          true,
          "Usuario cadastrado com sucesso."));
}