using FluentValidation;
using Ecommerce.DTOs.Product;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nama produk wajib diisi");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Harga harus lebih dari 0");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock tidak boleh negatif");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori wajib diisi");
    }
}