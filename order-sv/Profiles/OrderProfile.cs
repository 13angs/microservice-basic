using AutoMapper;
using order_sv.DTOs;
using order_sv.Models;

namespace order_sv.Profiles {
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderModel>()
                .ReverseMap();
        }
    }
}