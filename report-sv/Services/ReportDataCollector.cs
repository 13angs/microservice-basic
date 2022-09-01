using Newtonsoft.Json;
using Plain.RabbitMQ;
using report_sv.Models;

namespace report_sv.Services {
    public class ReportDataCollector : IHostedService
    {
        private readonly ISubscriber subscriber;
        private readonly IServiceProvider serviceProvider;

        public ReportDataCollector(ISubscriber subscriber, IServiceProvider serviceProvider)
        {
            this.subscriber = subscriber;
            this.serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriber.Subscribe(ProcessMessage);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool ProcessMessage(string message, IDictionary<string, object> headers)
        {
            Product? product = JsonConvert.DeserializeObject<Product>(message);

            // Use scoped services within a BackgroundService
            // https://docs.microsoft.com/en-us/dotnet/core/extensions/scoped-service
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                ReportContext context = scope.ServiceProvider.GetRequiredService<ReportContext>();

                if(!String.IsNullOrEmpty(product!.Name))
                {
                    Console.WriteLine($"Adding {message}");
                    context.Products.Add(product);
                    context.SaveChanges();
                }else{
                    Console.WriteLine("Failed creating product!");
                }
            }

            return true;
        }
    }
}