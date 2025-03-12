using Mapster;
using Products.Domain.Messaging.Products;
using ServiceOrder.Domain.Entities;

namespace ServiceOrder.Application.Consumers.Product;

public class ProductCreatedConsumerProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductCreated, ServiceOrderItem>()
            .ConstructUsing(p => new ServiceOrderItem(p.Id, p.Price));
    }
}