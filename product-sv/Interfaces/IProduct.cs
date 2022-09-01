using Microsoft.AspNetCore.Mvc;
using product_sv.DTOs;

namespace product_sv.Interfaces 
{
    public interface IProductService
    {
        public ActionResult<IEnumerable<ProductModel>> Get();
        public Task<ActionResult<ProductModel>> Create(ProductModel model);
    }
}