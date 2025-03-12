using Mapster;
using MassTransit;
using Products.Domain.Messaging.Products;
using Serilog;
using ServiceOrder.Domain.Entities;
using ServiceOrder.ORM.Context;

namespace ServiceOrder.Application.Consumers.Product;

public class ProductCreatedConsumer(ServiceOrderDbContext dbContext) : IConsumer<ProductCreated>
{
    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        Log.Information("Processando mensagem {messageId}.", context.Message.CorrelationId);

        var product = context.Message;
        var serviceOrderItem = product.Adapt<ServiceOrderItem>();

        var serviceOrder = new Domain.Entities.ServiceOrder(new List<ServiceOrderItem> { serviceOrderItem });

        Log.Information("Inserindo nova ordem de serviço.");

        dbContext.ServiceOrders.Add(serviceOrder);

        var success = await dbContext.SaveChangesAsync(context.CancellationToken) > 0;

        if (!success)
            Log.Error("Ocorreu uma falha ao inserir a ordem de serviço. CorrelationId da mensagem: {correlationId}", context.CorrelationId);
    }
}