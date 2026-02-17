using Ecommerce.DTOs.User;

namespace Ecommerce.DTOs.Auth;
public class AuthResponseDto
{
  public string Token { get; set; } = "";
    public DateTime Expiration { get; set; }
    public UserResponseDto User { get; set; } = null!;}
