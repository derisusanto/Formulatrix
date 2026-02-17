using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.DTOs.Category;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var result = await _categoryService.CreateAsync(dto);
        if (!result.Success) return BadRequest(result.Message);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _categoryService.GetByIdAsync(id);
        if (!result.Success) return NotFound(result.Message);

        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CreateCategoryDto dto)
    {
        var result = await _categoryService.UpdateAsync(id, dto);
        if (!result.Success) return NotFound(result.Message);

        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.DeleteAsync(id);
        if (!result.Success) return NotFound(result.Message);

        return NoContent();
    }
}
