namespace Ecommerce.DTOs.product;
public class ProductCreateDto
{
    public string Name {get; set;} = "";
    public int Price {get; set;}
    public int Stock {get; set;}
    public Guid CategoryId {get; set;}
}