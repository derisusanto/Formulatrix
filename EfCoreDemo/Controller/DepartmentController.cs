using Microsoft.AspNetCore.Mvc;
using EfCoreDemo.Data;
using EfCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentController(AppDbContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    public async Task<IActionResult> Create(Department dept)
    {

    
        _context.Departments.Add(dept);
        await _context.SaveChangesAsync();
        return Ok(dept);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.Departments.ToListAsync();
        return Ok(data);
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dept = await _context.Departments.FindAsync(id);
        if (dept == null) return NotFound();

        _context.Departments.Remove(dept);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
