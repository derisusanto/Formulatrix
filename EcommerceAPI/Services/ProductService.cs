// using Ecommerce.Models;
// using Ecommerce.Repositories.Interfaces;
// using Ecommerce.Services.Interfaces;

// namespace Ecommerce.Services;

// public class ProductService : IProductService
// {
//     private readonly IProductRepository _repo;

//     public ProductService(IProductRepository repo)
//     {
//         _repo = repo;
//     }

//     public async Task<List<Product>> GetAllAsync()
//     {
//         return await _repo.GetAllAsync();
//     }

//     public async Task<Product?> GetByIdAsync(Guid id)
//     {
//         return await _repo.GetByIdAsync(id);
//     }

//     public async Task<Product> CreateAsync(Product product)
//     {
//         return await _repo.CreateAsync(product);
//     }

//     public async Task<Product?> UpdateAsync(Guid id, Product product)
//     {
//         var existing = await _repo.GetByIdAsync(id);

//         if (existing == null)
//             return null;

//         existing.Name = product.Name;
//         existing.Price = product.Price;
//         existing.Stock = product.Stock;
//         existing.CategoryId = product.CategoryId;

//         return await _repo.UpdateAsync(existing);
//     }

//     public async Task<bool> DeleteAsync(Guid id)
//     {
//         var existing = await _repo.GetByIdAsync(id);

//         if (existing == null)
//             return false;

//         await _repo.SoftDeleteAsync(existing);
//         return true;
//     }
// }
