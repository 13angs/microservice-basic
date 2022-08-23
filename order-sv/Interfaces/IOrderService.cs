using Microsoft.AspNetCore.Mvc;
using order_sv.DTOs;

namespace order_sv.Interfaces
{
    public interface IOrderService
    {
        public ActionResult<IEnumerable<OrderModel>> Get();
    }
}