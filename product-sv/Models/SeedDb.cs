namespace product_sv.Models
{
    public class SeedDb{
        public static void Populate(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                Seed(scope.ServiceProvider.GetService<ProductContext>());
            }
        }

        public static void Seed(ProductContext? context)
        {
            if(!context!.Products.Any())
            {
                Console.WriteLine("Seeding...");
                IList<Product> products = new List<Product>(){
                    new Product{Id=1, ProductName="Produc1", Stock=50, Description="Product 1", Price=150},
                    new Product{Id=2, ProductName="Produc2", Stock=100, Description="Product 2", Price=300},
                    new Product{Id=3, ProductName="Produc3", Stock=200, Description="Product 3", Price=450},
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }else {
                Console.WriteLine("Faild Seeding...");
            }

        }

    }
}