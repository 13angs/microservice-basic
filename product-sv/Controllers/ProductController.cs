using Microsoft.AspNetCore.Mvc;
using product_sv.DTOs;
using product_sv.Interfaces;

namespace product_sv.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            ActionResult<IEnumerable<ProductModel>> models = productService.Get();
            return models;
        }
    }
}