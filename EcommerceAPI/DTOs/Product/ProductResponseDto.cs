using Ecommerce.DTOs.User;

namespace Ecommerce.DTOs.Product
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }


    }
}
