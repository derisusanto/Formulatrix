using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Model;

namespace Ecommerce.Data;

public class AppDbContext 
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }


    // MODEL CONFIGURATION
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // PRODUCT
        // builder.Entity<Product>(entity =>
        // {
        //     entity.Property(x => x.Name)
        //         .IsRequired()
        //         .HasMaxLength(150);

        //     entity.Property(x => x.Price)
        //         .HasPrecision(18, 2);

        //     entity.HasOne(x => x.Category)
        //         .WithMany(c => c.Products)
        //         .HasForeignKey(x => x.CategoryId)
        //         .OnDelete(DeleteBehavior.Restrict);

        //     entity.HasQueryFilter(x => !x.IsDeleted);
        // });


        // CATEGORY
        // builder.Entity<Category>(entity =>
        // {
        //     entity.Property(x => x.Name)
        //         .IsRequired()
        //         .HasMaxLength(100);

        //     entity.HasQueryFilter(x => !x.IsDeleted);
        // });


        // ORDER
        // builder.Entity<Order>(entity =>
        // {
        //     entity.Property(x => x.TotalPrice)
        //         .HasPrecision(18, 2);

        //     entity.HasOne(x => x.User)
        //         .WithMany(u => u.Orders)
        //         .HasForeignKey(x => x.UserId)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     entity.HasQueryFilter(x => !x.IsDeleted);
        // });


        // ORDER ITEM      
        // builder.Entity<OrderItem>(entity =>
        // {
        //     entity.Property(x => x.Price)
        //         .HasPrecision(18, 2);

        //     entity.HasOne(x => x.Order)
        //         .WithMany(o => o.Items)
        //         .HasForeignKey(x => x.OrderId);

        //     entity.HasOne(x => x.Product)
        //         .WithMany()
        //         .HasForeignKey(x => x.ProductId);

        //     entity.HasQueryFilter(x => !x.IsDeleted);
        // });
  
    }


  
    // AUTO AUDIT 
    // public override async Task<int> SaveChangesAsync(
    //     CancellationToken cancellationToken = default)
    // {
    //     var entries = ChangeTracker.Entries<BaseEntity>();

    //     foreach (var entry in entries)
    //     {
    //         if (entry.State == EntityState.Modified)
    //         {
    //             entry.Entity.UpdatedAt = DateTime.UtcNow;
    //         }
    //     }

    //     return await base.SaveChangesAsync(cancellationToken);
    // }

}
