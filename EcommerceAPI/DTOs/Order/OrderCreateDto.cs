namespace Ecommerce.DTOs.order;
public class OrderCreateDto
{
    public List<OrderItemDto> Items { get; set; } = new();
}
