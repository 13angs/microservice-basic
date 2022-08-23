using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using order_sv.DTOs;
using order_sv.Interfaces;
using order_sv.Models;

namespace order_sv.Services
{
    public class OrderService : ControllerBase, IOrderService
    {
        private readonly OrderContext context;
        private readonly IMapper mapper;

        public OrderService(OrderContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ActionResult<IEnumerable<OrderModel>> Get()
        {
            IEnumerable<Order> orders = context.Orders;

            IEnumerable<OrderModel> models = new List<OrderModel>();
            mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(orders, models);

            return Ok(models);
        }
    }
}