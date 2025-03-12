using MediatR;

namespace Products.Application.Products.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductResult>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}