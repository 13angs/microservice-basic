using Newtonsoft.Json;
using product_sv.Models;

namespace product_sv.DTOs
{
    public class ProductModel
    {

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