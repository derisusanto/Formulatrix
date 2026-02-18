using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.DTOs.Product;
using Ecommerce.Services.Interfaces;
using Ecommerce.Common.ServiceResult;
using FluentValidation;

namespace Ecommerce.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<CreateProductDto> _productValidator;

    public ProductController(
        IProductService service,
        IValidator<CreateProductDto> productValidator

        )
    {
        _productService = service;
        _productValidator = productValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        //panggil validator
        var validationResult = await _productValidator.ValidateAsync(dto);
        if (!validationResult.IsValid){
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                var ErrorResponse = ServiceResult<ProductResponseDto>.ErrorResult("Validation failed: " + string.Join(", ", errors));
                return BadRequest(ErrorResponse);
            }
        // Ambil userId dari token
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null) return Unauthorized();

        //conver dari jadi guid
        Guid userId = Guid.Parse(userIdString);
        //panggil service layer CreateAsync
        var result = await _productService.CreateAsync(dto, userId);

        //gagal kembalikan ini
        if (!result.Success) return BadRequest(result);
        //sukses kembalikan ini
        return Ok(result);
    }

   

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();
        return Ok(result);
    }
}

