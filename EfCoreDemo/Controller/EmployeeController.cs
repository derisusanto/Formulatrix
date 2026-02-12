
using Microsoft.AspNetCore.Mvc;
using EfCoreDemo.Data;
using EfCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using EfCoreDemo.DTOs;

namespace EfCoreDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    
[HttpPost]
public async Task<IActionResult> CreateEmployee(EmployeeInputDto dto)
{
   
    if (!_context.Departments.Any())
        return BadRequest("Belum ada Department. Tambahkan Department terlebih dahulu!");


    if (!_context.Departments.Any(d => d.Id == dto.DepartmentId))
        return BadRequest("DepartmentId tidak valid.");

    
    var emp = new Employee
    {
        Name = dto.Name,
        DepartmentId = dto.DepartmentId
    };

    _context.Employees.Add(emp);
    await _context.SaveChangesAsync();

   
    return Ok(emp);
}



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.Employees.ToListAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return NotFound();

        return Ok(emp);
    }

  
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee emp)
    {
        var existing = await _context.Employees.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Name = emp.Name;
        // existing.Position = emp.Position;
        // existing.Salary = emp.Salary;

        await _context.SaveChangesAsync();
        return Ok(existing);
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return NotFound();

        _context.Employees.Remove(emp);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
