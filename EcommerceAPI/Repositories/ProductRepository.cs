// using Microsoft.EntityFrameworkCore;
// using Ecommerce.Data;
// using Ecommerce.Models;
// using Ecommerce.Repositories.Interfaces;

// namespace Ecommerce.Repositories;

// public class ProductRepository : IProductRepository
// {
//     private readonly AppDbContext _context;

//     public ProductRepository(AppDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<List<Product>> GetAllAsync()
//     {
//         return await _context.Products
//             .Include(x => x.Category)
//             .ToListAsync();
//     }

//     public async Task<Product?> GetByIdAsync(Guid id)
//     {
//         return await _context.Products
//             .Include(x => x.Category)
//             .FirstOrDefaultAsync(x => x.Id == id);
//     }

//     public async Task<Product> CreateAsync(Product product)
//     {
//         _context.Products.Add(product);
//         await _context.SaveChangesAsync();
//         return product;
//     }

//     public async Task<Product> UpdateAsync(Product product)
//     {
//         _context.Products.Update(product);
//         await _context.SaveChangesAsync();
//         return product;
//     }

//     public async Task SoftDeleteAsync(Product product)
//     {
//         product.IsDeleted = true;
//         await _context.SaveChangesAsync();
//     }
// }
