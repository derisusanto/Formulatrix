
using FluentValidation;
using Ecommerce.DTOs.User;

namespace Ecommerce.Validators;
public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email wajib diisi")
            .EmailAddress().WithMessage("Format email tidak valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password wajib diisi");

    }
}