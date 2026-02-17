using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Model;
using Ecommerce.Repositories.Interfaces;

namespace Ecommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // CREATE: sekarang return Product
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // load relasi kalau mau mapping nanti
            await _context.Entry(product).Reference(p => p.Category).LoadAsync();
            await _context.Entry(product).Reference(p => p.Seller).LoadAsync();

            return product;
        }

        // GET ALL: tetap sama
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .ToListAsync();
        }
    }
}
