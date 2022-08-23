using Microsoft.AspNetCore.Mvc;
using order_sv.DTOs;
using order_sv.Interfaces;

namespace order_sv.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderModel>> Get()
        {
            ActionResult<IEnumerable<OrderModel>> models = orderService.Get();
            return models;   
        }
    }
}