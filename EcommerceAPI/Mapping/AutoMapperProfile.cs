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
        // ubah satu object ke object lain
        //ubah dari dto ke user
        CreateMap<UserRegisterDto, User>();
        //ubah dari user ke dto
        CreateMap<User, UserResponseDto>();
        
        CreateMap<Product, ProductResponseDto>();
        CreateMap<CreateProductDto, Product>();

        CreateMap<Category, CategoryResponseDto>();
        CreateMap<CreateCategoryDto, Category>();



    }
}
