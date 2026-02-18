using System.Text.Json.Serialization;

namespace EcommerUi.Models;

public class User { [JsonPropertyName("id")] public Guid Id { get; set; } [JsonPropertyName("fullName")] public string FullName { get; set; } = ""; [JsonPropertyName("email")] public string Email { get; set; } = ""; }
public class Category { [JsonPropertyName("id")] public Guid Id { get; set; } [JsonPropertyName("name")] public string Name { get; set; } = ""; }
public class Product { [JsonPropertyName("id")] public Guid Id { get; set; } [JsonPropertyName("name")] public string Name { get; set; } = ""; [JsonPropertyName("price")] public decimal Price { get; set; } [JsonPropertyName("stock")] public int Stock { get; set; } [JsonPropertyName("categoryId")] public Guid CategoryId { get; set; } [JsonPropertyName("categoryName")] public string CategoryName { get; set; } = ""; }
public class LoginModel { [JsonPropertyName("email")] public string Email { get; set; } = ""; [JsonPropertyName("password")] public string Password { get; set; } = ""; }
public class RegisterModel
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = "";

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = "";

    [JsonPropertyName("email")]
    public string Email { get; set; } = "";

    [JsonPropertyName("password")]
    public string Password { get; set; } = "";

    [JsonPropertyName("confirmPassword")]
    public string ConfirmPassword { get; set; } = "";
}
public class AuthResponse { [JsonPropertyName("token")] public string Token { get; set; } = ""; [JsonPropertyName("fullName")] public string FullName { get; set; } = ""; }
