using Newtonsoft.Json;

namespace product_sv.Models
{
    /// <summary>
    /// Represents a product.
    /// </summary>
    public class Product {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("stock")]
        public int Stock {get; set;}

        [JsonProperty("description")]
        public string? Description {get;set ;}

        [JsonProperty("price")]
        public decimal? Price {get;set;}
    }
}