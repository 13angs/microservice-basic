using Microsoft.AspNetCore.Mvc;
using report_sv.Models;

namespace report_sv.Controllers {
    
    [ApiController]
    [Route("api/reports")]
    public class ReportController: ControllerBase
    {
        private readonly ReportContext context;

        public ReportController(ReportContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<Product> products = context.Products;
            return Ok(products);
        }
    }
}