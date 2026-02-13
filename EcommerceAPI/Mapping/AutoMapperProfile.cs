using AutoMapper;
using Ecommerce.Models;
// using Ecommerce.DTOs.Product;
// using Ecommerce.DTOs.Category;
// using Ecommerce.DTOs.Order;
// using Ecommerce.DTOs.User;

namespace Ecommerce.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // // =======================
        // // PRODUCT
        // // =======================
        // CreateMap<ProductCreateDto, Product>();
        // CreateMap<ProductUpdateDto, Product>();
        // CreateMap<Product, ProductResponseDto>()
        //     .ForMember(dest => dest.CategoryName,
        //                opt => opt.MapFrom(src => src.Category!.Name));

        // // =======================
        // // CATEGORY
        // // =======================
        // CreateMap<CategoryCreateDto, Category>();
        // CreateMap<CategoryUpdateDto, Category>();
        // CreateMap<Category, CategoryResponseDto>();

        // // =======================
        // // ORDER
        // // =======================
        // CreateMap<OrderItemDto, OrderItem>();
        // CreateMap<OrderCreateDto, Order>();
        // CreateMap<Order, OrderResponseDto>()
        //     .ForMember(dest => dest.Items,
        //                opt => opt.MapFrom(src => src.Items));

        // =======================
        // USER
        // =======================
        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserResponseDto>();
    }
}
