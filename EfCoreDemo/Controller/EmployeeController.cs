
using Microsoft.AspNetCore.Mvc;
using EfCoreDemo.Data;
using EfCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;
using EfCoreDemo.DTOs.Request;
using EfCoreDemo.DTOs.Response;

namespace EfCoreDemo.Controllers;

[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    

[HttpPost]
public async Task<IActionResult> CreateEmployee(EmployeeRequest request)
{
  
    if (!_context.Departments.Any())
        return BadRequest("Belum ada Department. Tambahkan Department terlebih dahulu!");

 
    var department = await _context.Departments
        .FirstOrDefaultAsync(d => d.Id == request.DepartmentId);

    if (department == null)
        return BadRequest("DepartmentId tidak valid.");

  
    var emp = new Employee
    {
        Name = request.Name,
        DepartmentId = request.DepartmentId
    };

    _context.Employees.Add(emp);
    await _context.SaveChangesAsync(); 

    
    var dataEmployee = new EmployeeResponse
    {
        Id = emp.Id,
        Name = emp.Name,
        DepartmentId = emp.DepartmentId,
        DepartmentName = department.Name
    };

    return Ok(dataEmployee);
    // return Ok();
}

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.Employees
                .Include(e => e.Departement)
                .ToListAsync();

    return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var emp = await _context.Employees
            .Include(e => e.Departement)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (emp == null) return NotFound();

        return Ok(emp);
    }

  
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, EmployeeRequest request)
    {
        var existing = await _context.Employees.FindAsync(id);
        //  var existing = await _context.Employees
        // .Include(e => e.Department)
        // .FirstOrDefaultAsync(e => e.Id == id);
        
        if (existing == null) return NotFound();

        existing.Name = request.Name;
        existing.DepartmentId = request.DepartmentId;


        await _context.SaveChangesAsync();
        return Ok(existing);
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return NotFound();

        _context.Employees.Remove(emp);
        await _context.SaveChangesAsync();

        return Ok();

    //     var emp = await _context.Employees.FindAsync(id);

    // if (emp == null)
    //     return NotFound();

    // emp.IsDeleted = true;

    // await _context.SaveChangesAsync();

    // return Ok();
    }
}
