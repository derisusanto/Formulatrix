using Microsoft.AspNetCore.Mvc;
using EfCoreDemo.Data;
using EfCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using EfCoreDemo.DTOs.Request;
using EfCoreDemo.DTOs.Response;

namespace EfCoreDemo.Controllers;

[ApiController]
[Route("api/department")]
public class DepartmentController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentController(AppDbContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    public async Task<IActionResult> Create(DepartmentRequest request)
    {

        var depart = new Department
        {
            Name = request.Name,
        };
        _context.Departments.Add(depart);
        await _context.SaveChangesAsync();
        return Ok(depart);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.Departments.ToListAsync();
        return Ok(data);
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var dept = await _context.Departments.FindAsync(id);
        if (dept == null) return NotFound();

        _context.Departments.Remove(dept);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
