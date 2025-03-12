using MediatR;
using ProductManagement.Common.WebApi;
using Products.Application.Models;

namespace Products.Application.Products.ListProducts;

public class ListProductsQuery : PagedRequestInputModel, IRequest<PaginatedList<ListProductsResult>>
{
    
}