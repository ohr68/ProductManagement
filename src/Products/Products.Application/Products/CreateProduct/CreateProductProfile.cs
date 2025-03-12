using Mapster;
using Products.Domain.Entities;
using Products.Domain.Messaging.Products;

namespace Products.Application.Products.CreateProduct;

public class CreateProductProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductCommand, Product>();
        config.NewConfig<Product, CreateProductResult>();
        config.NewConfig<Product, ProductCreated>();
    }
}