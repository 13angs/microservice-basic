using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using order_sv.DTOs;
using order_sv.Interfaces;
using order_sv.Models;
using Plain.RabbitMQ;

namespace order_sv.Services
{
    public class OrderService : ControllerBase, IOrderService
    {
        private readonly OrderContext context;
        private readonly IMapper mapper;
        private readonly IPublisher publisher;

        public OrderService(OrderContext context, IMapper mapper, IPublisher publisher)
        {
            this.context = context;
            this.mapper = mapper;
            this.publisher = publisher;
        }
        public ActionResult<IEnumerable<OrderModel>> Get()
        {
            IEnumerable<Order> orders = context.Orders;

            IEnumerable<OrderModel> models = new List<OrderModel>();
            mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(orders, models);

            return Ok(models);
        }

        public async Task<ActionResult> Create(OrderModel model)
        {
            Order order = new Order();
            mapper.Map<OrderModel, Order>(model, order);

            await context.Orders.AddAsync(order);
            int result = await context.SaveChangesAsync();

            if(result > 0)
            {
                publisher.Publish(JsonConvert.SerializeObject(order), "report.order", null);
                Console.WriteLine("Order created successfully");
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(StatusCodes.Status409Conflict);
        }
    }
}