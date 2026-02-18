using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Model;

public class User : IdentityUser<Guid>
{
        // public string? FullName { get; set; }
        // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        
        
}
