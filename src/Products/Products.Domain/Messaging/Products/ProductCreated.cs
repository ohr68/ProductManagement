namespace Products.Domain.Messaging.Products;

public class ProductCreated : Message
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    
    public DateTime CreatedAt { get; set; }
}