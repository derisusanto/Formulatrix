namespace Ecommerce.DTOs.User;
public class UserResponseDto
{
  public Guid Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}