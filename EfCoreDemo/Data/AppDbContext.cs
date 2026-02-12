using Microsoft.EntityFrameworkCore;
using EfCoreDemo.Entities;

namespace EfCoreDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments {get;set;}
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeDetail> EmployeeDetails {get;set;}
   
}