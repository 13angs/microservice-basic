using Newtonsoft.Json;

namespace report_sv.Models
{
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