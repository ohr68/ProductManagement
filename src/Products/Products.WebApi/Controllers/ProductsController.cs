using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Common.WebApi;
using Products.Application.Products.CreateProduct;
using Products.Application.Products.ListProducts;

namespace Products.WebApi.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas à produtos
/// </summary>
/// <param name="mediator">Responsável por enviar os comandos e queries para seus respectivos handlers conforme contratos</param>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Lista os produtos de forma paginada
    /// </summary>
    /// <param name="query">Modelo de requisição paginada</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Lista paginada com os produtos encontrados</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedListResponse<ListProductsResult>), StatusCodes.Status200OK, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, contentType: "application/json")]
    public async Task<IActionResult> List([FromQuery] ListProductsQuery query, CancellationToken cancellationToken = default)
       => OkPaginated(await mediator.Send(query, cancellationToken));

    /// <summary>
    /// Cadastra um novo produto
    /// </summary>
    /// <param name="command">Modelo de requisição contendo os dados do produto a ser cadastrado</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Resultado da operação de insert com o Id do produto inserido</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResult>), StatusCodes.Status201Created, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, contentType: "application/json")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken = default)
        => Created(string.Empty, 
            new ApiResponseWithData<CreateProductResult>(await mediator.Send(command, cancellationToken),
            true,
            "Produto cadastrado com sucesso."));
}