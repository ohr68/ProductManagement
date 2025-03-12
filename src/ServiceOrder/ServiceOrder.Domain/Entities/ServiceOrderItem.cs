namespace ServiceOrder.Domain.Entities;

public class ServiceOrderItem
{
    public int Id { get; set; }
    public int ServiceOrderId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    
    public virtual ServiceOrder? ServiceOrder { get; set; }

    public ServiceOrderItem()
    {
        
    }

    public ServiceOrderItem(int productId, decimal price)
    {
        ProductId = productId;
        Price = price;
    }
}