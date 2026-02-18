
using FluentValidation;
using Ecommerce.DTOs.User;

namespace Ecommerce.Validators;

public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidator()
    {
        // RuleFor(x => x.FullName).NotEmpty().MaximumLength(30);
        // RuleFor(x => x.Email).NotEmpty().EmailAddress();
        // RuleFor(x => x.Password).NotEmpty()
        // .MinimumLength(6)
        // .Matches("[A-Z]").WithMessage("Harus ada huruf kapital")
        // .Matches("[a-z]").WithMessage("Harus ada huruf kecil")
        // .Matches("[0-9]").WithMessage("Harus ada angka");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters");
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Confirm password must match password");
    
    }
}
