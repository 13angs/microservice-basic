using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Plain.RabbitMQ;
using product_sv.DTOs;
using product_sv.Interfaces;
using product_sv.Models;

namespace product_sv.Services
{
    public class ProductService : ControllerBase, IProductService
    {
        private readonly ProductContext context;
        private readonly IMapper mapper;
        private readonly IPublisher publisher;

        public ProductService(ProductContext context, IMapper mapper, IPublisher publisher)
        {
            this.context = context;
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            IEnumerable<Product> products = context.Products;

            IList<ProductModel> models = new List<ProductModel>();

            mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products, models);
            return Ok(models);
        }

        public async Task<ActionResult<ProductModel>> Create(ProductModel model)
        {
            Product? product = await context.Products.FirstOrDefaultAsync(p => p.Name == model.Name);

            if (product!= null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new {Message="Product already exists.", status=StatusCodes.Status409Conflict});
            }

            product = new Product();
            mapper.Map<ProductModel, Product>(model, product);
            context.Products.Add(product);
            int result = await context.SaveChangesAsync();

            if(result > 0)
            {
                publisher.Publish(JsonConvert.SerializeObject(product), "report.product", null);
                Console.WriteLine("Product created successfully");
                return StatusCode(StatusCodes.Status201Created, new {Message="Product created successfully.", status=StatusCodes.Status201Created});
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new {Message="Product creation failed.", status=StatusCodes.Status500InternalServerError});
        }
        
        // public ActionResult<IEnumerable<ProductModel>> Get()
        // {
        //     IEnumerable<Product> products = context.Products;

        //     IList<ProductModel> models = new List<ProductModel>();

        //     mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products, models);
            
        //     var factory = new ConnectionFactory{
        //         Uri=new Uri("amqp://guest:guest@msb-rabitmq-management:5672")
        //     };
        //     using var connection = factory.CreateConnection();
        //     using var channel = connection.CreateModel();

        //     QueueProducer.Publish(channel);
        //     DirectExchangePublisher.Publish(channel);
        //     TopicExchangePublisher.Publish(channel);
        //     HeaderExchangePublisher.Publish(channel);
        //     FanoutExchangePublisher.Publish(channel);

        //     return Ok(models);
        // }
    }
}