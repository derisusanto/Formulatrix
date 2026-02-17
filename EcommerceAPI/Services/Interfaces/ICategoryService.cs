using Ecommerce.DTOs.Category;
using Ecommerce.Common.ServiceResult;


namespace Ecommerce.Services.Interfaces;
public interface ICategoryService
{
        Task<ServiceResult<CategoryResponseDto>> CreateAsync(CreateCategoryDto dto);
        Task<ServiceResult<List<CategoryResponseDto>>> GetAllAsync();
        Task<ServiceResult<CategoryResponseDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<CategoryResponseDto>> UpdateAsync(Guid id, CreateCategoryDto dto);
        Task<ServiceResult<bool>> DeleteAsync(Guid id);
}


