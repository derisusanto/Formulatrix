using Ecommerce.DTOs.User;

namespace Ecommerce.DTOs.Product;

public class ProductResponseAll
{
    public UserResponseDto Seller { get; set; } = new();
    public List<ProductResponseDto> Products { get; set; } = new();
}