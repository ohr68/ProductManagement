using ServiceOrder.Domain.Enums;

namespace ServiceOrder.Domain.Entities;

public class ServiceOrder
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public virtual ICollection<ServiceOrderItem>? Items { get; set; }

    public ServiceOrder()
    {
        Created();
    }

    public ServiceOrder(IEnumerable<ServiceOrderItem> items)
    {
        Created();
        SetTotalPrice();
    }

    private void Created()
    {
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    private void SetTotalPrice()
    {
        TotalPrice = Items!.Sum(x => x.Price);
    }
}