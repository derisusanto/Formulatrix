using Ecommerce.Model;

namespace Ecommerce.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);
    Task<List<Product>> GetAllAsync();
}
