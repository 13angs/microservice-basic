using Newtonsoft.Json;

namespace order_sv.Models
{
    public class Order{

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name {get; set; }

        [JsonProperty("amount")]
        public int Amount {get; set;}
    }
}