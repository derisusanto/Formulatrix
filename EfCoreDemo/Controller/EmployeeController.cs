// using Microsoft.AspNetCore.Mvc;
// using EfCoreDemo.Data;
// using EfCoreDemo.Entities;

// namespace EfCoreDemo.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class EmployeeController : ControllerBase
// {
//     private readonly AppDbContext _context;

//     public EmployeeController(AppDbContext context)
//     {
//         _context = context;
//     }

//     [HttpPost]
//     public async Task<IActionResult> Create(Employee emp)
//     {
//         _context.Employees.Add(emp);
//         await _context.SaveChangesAsync();
//         return Ok(emp);
//     }

//     [HttpGet]
//     public async Task<IActionResult> GetAll()
//     {
//         return Ok(_context.Employees.ToList());
//     }
    
// }
using Microsoft.AspNetCore.Mvc;
using EfCoreDemo.Data;
using EfCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;

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

    // ✅ CREATE
    [HttpPost]
    public async Task<IActionResult> Create(Employee emp)
    {
        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();
        return Ok(emp);
    }

    // ✅ READ ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.Employees.ToListAsync();
        return Ok(data);
    }

    // ✅ READ BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return NotFound();

        return Ok(emp);
    }

    // ✅ UPDATE
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

    // ✅ DELETE
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
