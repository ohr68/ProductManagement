using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Common.Models;
using ProductManagement.Common.WebApi;
using ServiceOrder.ORM.Context;

namespace ServiceOrder.Application.ServiceOrders.ListServiceOrders;

public class ListServiceOrderHandler(ServiceOrderDbContext context, IValidator<PagedRequestInputModel> validator)
    : IRequestHandler<ListServiceOrderQuery, PaginatedList<ListServiceOrderResult>>
{
    public async Task<PaginatedList<ListServiceOrderResult>> Handle(ListServiceOrderQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var serviceOrders = await context.ServiceOrders
            .Include(s => s.Items)
            .ToListAsync(cancellationToken);

        return PaginatedList<ListServiceOrderResult>.Create(serviceOrders.Adapt<IEnumerable<ListServiceOrderResult>>(),
            request.PageIndex, request.PageSize, cancellationToken);
    }
}