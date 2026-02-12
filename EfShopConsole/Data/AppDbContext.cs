using Microsoft.EntityFrameworkCore;
using EfShopJaya.Model;

namespace EfShopJaya.Data;

public class AppDbContext : DbContext 
{
    public DbSet<Product> Products  {get; set;}
    public DbSet<Category> Categories {get;set;}

       protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=shop.db");
    }
}