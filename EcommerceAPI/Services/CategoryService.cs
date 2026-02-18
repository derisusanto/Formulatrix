using AutoMapper;
using Ecommerce.Common.ServiceResult;
using Ecommerce.DTOs.Category;
using Ecommerce.Model;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<CategoryResponseDto>> CreateAsync(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _repository.AddAsync(category);

        return ServiceResult<CategoryResponseDto>.SuccessResult(_mapper.Map<CategoryResponseDto>(category));
    }

    public async Task<ServiceResult<List<CategoryResponseDto>>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        var data = _mapper.Map<List<CategoryResponseDto>>(categories);
        return ServiceResult<List<CategoryResponseDto>>.SuccessResult(data);
    }

    public async Task<ServiceResult<CategoryResponseDto>> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null) return ServiceResult<CategoryResponseDto>.ErrorResult("Category not found");

        return ServiceResult<CategoryResponseDto>.SuccessResult(_mapper.Map<CategoryResponseDto>(category));
    }

    public async Task<ServiceResult<CategoryResponseDto>> UpdateAsync(Guid id, CreateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null) return ServiceResult<CategoryResponseDto>.ErrorResult("Category not found");

        // Mapping DTO → Entity (update)
        var updatedCategory = _mapper.Map(dto, category); 
        // Simpan ke database
        await _repository.UpdateAsync(category);

        // Mapping Entity → DTO untuk response
        var categoryDto = _mapper.Map<CategoryResponseDto>(category);

        // Return hasil sukses
        return ServiceResult<CategoryResponseDto>.SuccessResult(categoryDto, "Category berhasil diupdate");

    }

    public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null) return ServiceResult<bool>.ErrorResult("Category not found");

        await _repository.DeleteAsync(category);
        return ServiceResult<bool>.SuccessResult(true);
    }
}
