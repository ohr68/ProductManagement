using Mapster;
using ServiceOrder.Domain.Entities;

namespace ServiceOrder.Application.ServiceOrders.ListServiceOrders;

public class ListServiceOrderProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Entities.ServiceOrder, ListServiceOrderResult>()
            .Map(dest => dest.Items, src => src.Items);

        config.NewConfig<ServiceOrderItem, ItemResult>();
    }
}