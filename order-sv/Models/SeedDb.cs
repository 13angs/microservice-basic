using System.Text.Json;

namespace order_sv.Models {
    public static class SeedDb {
        public static void Populate(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetService<OrderContext>());
            }
        }

        public static void SeedData(OrderContext? context)
        {
            if(!context!.Orders.Any())
            {
                Console.WriteLine("Seeding order...");
                IList<Order> orders = new List<Order>(){
                    new Order(){Id=1, Name="Order1"},
                    new Order(){Id=2, Name="Order2"},
                    new Order(){Id=3, Name="Order3"},
                };

                string strOrder = JsonSerializer.Serialize<IList<Order>>(orders);
                Console.WriteLine(strOrder);
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }else{
                Console.WriteLine("Order Exist...");
            }
        }
    }
}