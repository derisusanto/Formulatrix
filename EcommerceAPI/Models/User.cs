using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Model;

public class User : IdentityUser<Guid>
{
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    // public List<Order> Orders { get; set; } = new();
}
