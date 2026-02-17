using AutoMapper;
using Ecommerce.Model;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.User;
using Ecommerce.DTOs.Category;

namespace Ecommerce.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {

        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserResponseDto>();
        
        CreateMap<Product, ProductResponseDto>();
        CreateMap<CreateProductDto, Product>();

        CreateMap<Category, CategoryResponseDto>();
        CreateMap<CreateCategoryDto, Category>();



    }
}
