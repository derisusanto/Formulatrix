using Ecommerce.DTOs.Category;
using FluentValidation;

namespace Ecommerce.Validators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(10).WithMessage("Category name cannot exceed 100 characters");
    }
}
