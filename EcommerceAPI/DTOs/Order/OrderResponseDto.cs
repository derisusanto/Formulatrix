namespace Ecommerce.DTOs.order;
public class OrderResponseDto
{
    public Guid Id {get; set;}
    public string Username {get; set;} = "";
    public decimal TotalPrice {get;set;}
    public List<OrderItemDto> Items {get; set;}= new();
}