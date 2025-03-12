using MediatR;
using ProductManagement.Common.Models;
using ProductManagement.Common.WebApi;

namespace ServiceOrder.Application.ServiceOrders.ListServiceOrders;

public class ListServiceOrderQuery : PagedRequestInputModel, IRequest<PaginatedList<ListServiceOrderResult>>
{
    
}