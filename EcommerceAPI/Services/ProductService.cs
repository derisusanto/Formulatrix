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
        return ServiceResult<ProductResponseDto>.SuccessResult(productDto, "Produk berhasil dibuat");
    }
    catch (Exception ex)
    {
        // Jika terjadi error, kembalikan response gagal
        return ServiceResult<ProductResponseDto>.ErrorResult($"Gagal membuat produk: {ex.Message}");
    }
    }

    public async Task<ServiceResult<ProductResponseAll>> GetAllAsync(Guid? categoryId = null, string? sellerRole = null)
    {
        var products = await _repo.GetAllAsync();

        if (categoryId.HasValue)
            products = products.Where(p => p.CategoryId == categoryId.Value).ToList();

        var productDtos = _mapper.Map<List<ProductResponseDto>>(products);
        var response = new ProductResponseAll { Products = productDtos };

        if (products.Any())
        {
            var sellerEntity = await _userManager.FindByIdAsync(products.First().SellerId.ToString());
            if (sellerEntity != null)
            {
                response.Seller = new UserResponseDto
                {
                    Id = sellerEntity.Id,
                    FullName = sellerEntity.FullName ?? "",
                    Email = sellerEntity.Email ?? "",
                    CreatedAt = sellerEntity.CreatedAt
                };
            }
        }

        return ServiceResult<ProductResponseAll>.SuccessResult(response, "Data produk berhasil diambil");
    }

}
