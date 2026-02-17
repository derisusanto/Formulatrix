using AutoMapper;
using Microsoft.AspNetCore.Identity;

using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.DTOs.Product;
using Ecommerce.Model;
using Ecommerce.DTOs.User;
using Ecommerce.Common.ServiceResult;
using FluentValidation;
using System.ComponentModel;

namespace Ecommerce.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateProductDto> _validator;
    private readonly UserManager<User> _userManager;

    public ProductService(
        IProductRepository repo,
        IMapper mapper,
        IValidator<CreateProductDto> validator,
        UserManager<User> userManager )
    {
        _repo = repo;
        _mapper = mapper;
        _validator = validator;
        _userManager = userManager;
    }

    public async Task<ServiceResult<ProductResponseDto>> CreateAsync(CreateProductDto dto, Guid sellerId)
    {
        // Validasi input
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            return ServiceResult<ProductResponseDto>.ErrorResult(
                string.Join(", ", validation.Errors.Select(e => e.ErrorMessage))
            );

        var product = _mapper.Map<Product>(dto);
        product.Id = Guid.NewGuid();
        product.SellerId = sellerId;

        try
        {
            await _repo.AddAsync(product);

            var productDto = _mapper.Map<ProductResponseDto>(product);
            return ServiceResult<ProductResponseDto>.SuccessResult(productDto, "Produk berhasil dibuat");
        }
        catch (Exception ex)
        {
            return ServiceResult<ProductResponseDto>.ErrorResult($"Gagal membuat produk: {ex.Message}");
        }
    }

public async Task<ServiceResult<ProductResponseAll>> GetAllAsync(Guid? categoryId = null, string? sellerRole = null)
{
    // ambil semua produk
    var products = await _repo.GetAllAsync();

    // filter kategori
    if (categoryId.HasValue)
        products = products.Where(p => p.CategoryId == categoryId.Value).ToList();

    // cek kosong
    if (!products.Any())
        return ServiceResult<ProductResponseAll>.ErrorResult("Produk tidak ditemukan");

  var sellerEntity = await _userManager.FindByIdAsync(products.First().SellerId.ToString());
// var roles = await _userManager.GetRolesAsync(sellerEntity);
    
    // Mapping ke DTO
    var sellerDto = new UserResponseDto
    {
        Id = sellerEntity.Id,
        FullName = sellerEntity.FullName ?? "",
        Email = sellerEntity.Email ?? "",
        CreatedAt = sellerEntity.CreatedAt,
        // Roles = roles.ToList() // semua role seller
    };

    var productDtos = _mapper.Map<List<ProductResponseDto>>(products);

    var response = new ProductResponseAll
    {
        Seller = sellerDto,
        Products = productDtos
    };

    return ServiceResult<ProductResponseAll>.SuccessResult(response, "Data produk berhasil diambil");
}

}
