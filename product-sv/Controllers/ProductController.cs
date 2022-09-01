using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Plain.RabbitMQ;
using product_sv.DTOs;
using product_sv.Interfaces;

namespace product_sv.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IPublisher publisher;

        public ProductController(IProductService productService, IPublisher publisher)
        {
            this.productService = productService;
            this.publisher = publisher;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            ActionResult<IEnumerable<ProductModel>> models = productService.Get();
            return models;
        }

        [HttpPost]
        public ActionResult Create([FromBody] ProductModel model)
        {
            // insert into database
            // insert into queue
            publisher.Publish(JsonConvert.SerializeObject(model), "report.product", null);
            return Ok(model);
        }
    }
}