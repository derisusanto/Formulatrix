using Ecommerce.Model;
namespace Ecommerce.Repositories.Interfaces;
public interface ICategoryRepository
{

    Task AddAsync(Category category);
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);
}
