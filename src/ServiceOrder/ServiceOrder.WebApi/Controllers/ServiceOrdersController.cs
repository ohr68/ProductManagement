using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Common.WebApi;
using ServiceOrder.Application.ServiceOrders.ListServiceOrders;

namespace ServiceOrder.WebApi.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas à ordens de serviço
/// </summary>
/// <param name="mediator">Responsável por enviar os comandos e queries para seus respectivos handlers conforme contratos</param>
[Authorize]
[ApiController]
[Route("api/orders")]
public class ServiceOrdersController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Lista as ordens de serviço de forma paginada
    /// </summary>
    /// <param name="query">Modelo de requisição paginada</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Lista paginada com as ordens de serviço encontradas</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedListResponse<ListServiceOrderResult>), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, contentType: "application/json")]
    public async Task<IActionResult> List([FromQuery] ListServiceOrderQuery query, CancellationToken cancellationToken = default)
      => OkPaginated(await mediator.Send(query, cancellationToken));
}