using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace product_sv.Services
{
    public static class DirectExchangePublisher
    {
        /// <summary>
        /// Gets or sets the <see cref="DirectExchange"/>.
        /// </summary>
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000}
            };
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct, arguments: ttl);
            int count = 0;

            // while(true)
            // {
            //     count++;
            //     Thread.Sleep(1000);
            // }
            var message = new {Name="Producer", Message=$"Producer: {count}", ExchangeType="Direct"};
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("demo-direct-exchange", "account.init", null, body);
        }
    }
}