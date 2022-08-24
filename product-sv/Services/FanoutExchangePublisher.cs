using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace product_sv.Services
{
    public static class FanoutExchangePublisher
    {
        /// <summary>
        /// Gets or sets the <see cref="fanoutExchange"/>.
        /// </summary>
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000}
            };
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, arguments: ttl);
            int count = 0;

            // while(true)
            // {
            //     count++;
            //     Thread.Sleep(1000);
            // }
            var message = new {Name="Producer", Message=$"Producer: {count}", ExchangeType="Fanout"};
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object>(){{ "account", "init"}};

            channel.BasicPublish("demo-fanout-exchange", string.Empty, properties, body);
        }
    }
}