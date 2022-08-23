using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using product_sv.DTOs;
using product_sv.Interfaces;
using product_sv.Models;

namespace product_sv.Services
{
    public class ProductService : ControllerBase, IProductService
    {
        private readonly ProductContext context;
        private readonly IMapper mapper;

        public ProductService(ProductContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            IEnumerable<Product> products = context.Products;

            IList<ProductModel> models = new List<ProductModel>();

            mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products, models);

            return Ok(models);
        }
    }
}