namespace Ecommerce.DTOs.User;
public class UserRegisterDto
{
    // public string FullName {get; set;}=string.Empty;
    // public string Email {get; set;} = string.Empty;
    // public string Password {get; set;}=string.Empty;

    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Password {get; set;}=string.Empty;
    public string ConfirmPassword {get; set;}=string.Empty;
}