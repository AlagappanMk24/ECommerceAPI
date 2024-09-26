using AutoMapper;
using ECommerce.Application.DTOs.BrandDTO;
using ECommerce.Application.DTOs.CartDTO;
using ECommerce.Application.DTOs.CategoryDTO;
using ECommerce.Application.DTOs.OrderDTO;
using ECommerce.Application.DTOs.PaymentDTO;
using ECommerce.Application.DTOs.ProductDTO;
using ECommerce.Application.DTOs.ReviewDTO;
using ECommerce.Application.DTOs.UserDto;
using ECommerce.Application.DTOs.UserDTO;
using ECommerce.Application.DTOs.WishlistDTO;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
            CreateMap<UserDto, ApplicationUser>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Brand, src => src.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.Category, src => src.MapFrom(src => src.Category!.Name))
                .ReverseMap();
            CreateMap<Product, AddProductDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product!.Name))
                .ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product!.Name))
                .ReverseMap();

            CreateMap<Wishlist, WishlistDto>().ReverseMap();
            CreateMap<WishlistItem, WishlistItemDto>()
                .ForMember(dest => dest.WishlistName, src => src.MapFrom(src => src.Wishlist!.Name))
                .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product!.Name))
                .ReverseMap();

            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product!.Name))
                .ReverseMap();

            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
