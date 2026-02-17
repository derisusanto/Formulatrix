using Ecommerce.DTOs.Product;
using Ecommerce.Common.ServiceResult;

namespace Ecommerce.Services.Interfaces;
public interface IProductService
  {
      Task<ServiceResult<ProductResponseDto>> CreateAsync(CreateProductDto dto, Guid sellerId);
      Task<ServiceResult<ProductResponseAll>> GetAllAsync(Guid? categoryId = null, string? sellerRole = null);
  }




