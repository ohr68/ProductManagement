using MediatR;
using ProductManagement.Common.Models;
using ProductManagement.Common.WebApi;

namespace Products.Application.Products.ListProducts;

public class ListProductsQuery : PagedRequestInputModel, IRequest<PaginatedList<ListProductsResult>>
{
    
}