using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models.user;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = "";

    public List<Order> Orders { get; set; } = new();
}