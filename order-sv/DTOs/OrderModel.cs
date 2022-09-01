using Newtonsoft.Json;

namespace order_sv.DTOs
{
    public class OrderModel 
    {
        [JsonProperty("name")]
        public string? Name {get; set; }

        [JsonProperty("amount")]
        public int Amount {get; set;}
    }
}