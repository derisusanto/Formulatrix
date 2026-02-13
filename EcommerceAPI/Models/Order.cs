namespace Ecommerce.Models;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public decimal TotalPrice { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}
