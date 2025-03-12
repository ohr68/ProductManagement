using ServiceOrder.Domain.Enums;

namespace ServiceOrder.Application.ServiceOrders.ListServiceOrders;

public class ListServiceOrderResult
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public IEnumerable<ItemResult>? Items { get; set; }
}

public class ItemResult
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
}