using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Model;

namespace Ecommerce.Data;

// DbContext utama aplikasi + support ASP.NET Identity (User & Role)
public class AppDbContext 
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    //set data ke database produk dan category
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }


    // MODEL CONFIGURATION
  protected override void OnModelCreating(ModelBuilder builder)
{
    // Panggil konfigurasi Identity default
    base.OnModelCreating(builder);

    // CONFIGURE PRODUCT
    builder.Entity<Product>(entity =>
    {
        // Set primary key (sebenarnya EF otomatis tahu kalau namanya Id)
        entity.HasKey(p => p.Id);

        // Name wajib diisi & maksimal 30 karakter
        entity.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(30);

        // Atur precision decimal supaya tidak error di database
        entity.Property(p => p.Price)
            .HasPrecision(18, 2);

        // Relasi Product → Category
        // Satu Product punya satu Category
        // Satu Category punya banyak Product
        entity.HasOne(p => p.Category)
            .WithMany() // TANPA c => c.Products
            .HasForeignKey(p => p.CategoryId);
            // Restrict = tidak boleh hapus Category kalau masih ada Product

        // Relasi Product → Seller (User)
        entity.HasOne(p => p.Seller)
            .WithMany()
            .HasForeignKey(p => p.SellerId)
            .OnDelete(DeleteBehavior.Cascade);
            // Cascade = kalau user dihapus, product ikut terhapus
    });

    // CONFIGURE CATEGORY
    builder.Entity<Category>(entity =>
    {
        // Primary key
        entity.HasKey(c => c.Id);
        // Name wajib & maksimal 100 karakter
        entity.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(20);
    });
}
}
