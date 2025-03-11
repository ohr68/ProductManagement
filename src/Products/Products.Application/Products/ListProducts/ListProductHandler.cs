using FluentValidation;
using Mapster;
using MediatR;
using ProductManagement.Common.WebApi;
using Products.Application.Models;
using Products.Domain.Entities;
using Products.Domain.Interfaces;

namespace Products.Application.Products.ListProducts;

public class ListProductHandler(
    IRepository<Product> productRepository,
    IValidator<PagedRequestInputModel> validator)
    : IRequestHandler<ListProductsQuery, PaginatedList<ListProductsResult>>
{
    public async Task<PaginatedList<ListProductsResult>> Handle(ListProductsQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var products = await productRepository.GetAllAsync(cancellationToken);

        return PaginatedList<ListProductsResult>.Create(products.Adapt<IEnumerable<ListProductsResult>>(),
            request.PageIndex, request.PageSize, cancellationToken);
    }
}