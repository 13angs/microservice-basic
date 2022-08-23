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
                    new Product{Id=1, Name="Produc1"},
                    new Product{Id=2, Name="Produc2"},
                    new Product{Id=3, Name="Produc3"},
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }else {
                Console.WriteLine("Faild Seeding...");
            }

        }

    }
}