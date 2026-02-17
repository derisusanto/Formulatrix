using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.DTOs.Product;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService service)
    {
        _productService = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdString == null)
            return Unauthorized();

        Guid userId = Guid.Parse(userIdString);

        await _productService.CreateAsync(dto, userId);

        return Ok("Product created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

}

