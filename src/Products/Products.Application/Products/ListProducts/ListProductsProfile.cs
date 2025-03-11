using Mapster;
using Products.Domain.Entities;

namespace Products.Application.Products.ListProducts;

public class ListProductsProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ListProductsResult>();
    }
}