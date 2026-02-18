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
        _userManager = userManager;
    }

    public async Task<ServiceResult<ProductResponseDto>> CreateAsync(CreateProductDto dto, Guid sellerId)
    {
        // Mapping DTO ke Entity
    var product = _mapper.Map<Product>(dto);

    // Set Id baru dan SellerId
    product.Id = Guid.NewGuid();
    product.SellerId = sellerId;

    try
    {
        // Simpan ke database
        await _repo.AddAsync(product);

        // Mapping entity kembali ke DTO untuk response
        var productDto = _mapper.Map<ProductResponseDto>(product);
        // Kembalikan response sukses
        return ServiceResult<ProductResponseDto>.SuccessResult(productDto, "Product created successfully");
    }
    catch (Exception ex)
    {
        // Jika terjadi error, kembalikan response gagal
        return ServiceResult<ProductResponseDto>.ErrorResult($"Failed to create product : {ex.Message}");
    }
    }

   
    public async Task<ServiceResult<ProductResponseAll>> GetAllAsync(Guid? categoryId = null, string? sellerRole = null)
    {
        var products = await _repo.GetAllAsync();

        // filter kategori jika ada
        if (categoryId.HasValue)
            products = products.Where(p => p.CategoryId == categoryId.Value).ToList();

        // mapping produk
        var productDtos = _mapper.Map<List<ProductResponseDto>>(products);

        // ambil seller dari product pertama jika ada
        UserResponseDto? sellerDto = null;
        if (products.FirstOrDefault() is { } firstProduct)
        {
            var sellerEntity = await _userManager.FindByIdAsync(firstProduct.SellerId.ToString());
            var roles = await _userManager.GetRolesAsync(sellerEntity);
            
            // Console.WriteLine($"Entity => {roles}");
            if (sellerEntity != null)
            {
                sellerDto = _mapper.Map<UserResponseDto>(sellerEntity);
            }
        }

        var response = new ProductResponseAll
        {
            Products = productDtos ?? [],
            Seller = sellerDto
        };

        return ServiceResult<ProductResponseAll>.SuccessResult(response, "Product data retrieved successfully");
    }

}
