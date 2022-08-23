using AutoMapper;
using product_sv.DTOs;
using product_sv.Models;

namespace product_sv.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>()
                .ReverseMap();
        }
    }
}