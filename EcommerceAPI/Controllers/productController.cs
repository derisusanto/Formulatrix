using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.DTOs.Product;
using Ecommerce.Services.Interfaces;
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
        var validationResult = await _productValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        // Ambil userId dari token
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdString == null) return Unauthorized();

        Guid userId = Guid.Parse(userIdString);
        var result = await _productService.CreateAsync(dto, userId);

        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // For now, let's assume we need this for CreatedAtAction. 
        // We'll need to define it in IProductService too if not already there.
        // If not there, we'll implement a temporary list filter or just return 200.
        return Ok(new { success = true, message = "Endpoint stub for redirection" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();
        return Ok(result);
    }
}

