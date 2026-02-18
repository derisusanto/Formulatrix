namespace Ecommerce.DTOs.User;
public class UserProfileDto
{
  public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}