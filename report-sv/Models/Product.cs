using Newtonsoft.Json;

namespace report_sv.Models
{
    public class Product {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("product_name")]
        public string? ProductName { get; set; }

        [JsonProperty("stock")]
        public int Stock {get; set;}

        [JsonProperty("description")]
        public string? Description {get;set ;}

        [JsonProperty("price")]
        public decimal? Price {get;set;}
    }
}