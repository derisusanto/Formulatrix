using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.DTOs.Category;
using Ecommerce.Services.Interfaces;
using FluentValidation;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
        private readonly IValidator<CreateCategoryDto> _categoryValidator;

    public CategoryController(
        ICategoryService categoryService,            
        IValidator<CreateCategoryDto> categoryValidator
)
    {
        _categoryService = categoryService;
        _categoryValidator = categoryValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var validationResult = await _categoryValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _categoryService.CreateAsync(dto);
        if (!result.Success) return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
   
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _categoryService.GetByIdAsync(id);
        if (!result.Success) return NotFound(result);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CreateCategoryDto dto)
    {
   // Validasi input dulu
        var validationResult = await _categoryValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _categoryService.UpdateAsync(id, dto);
        if (!result.Success) return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.DeleteAsync(id);
        if (!result.Success) return NotFound(result);

        return Ok(result);
    }
}
