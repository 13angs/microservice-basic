using Newtonsoft.Json;

namespace report_sv.Models
{
    public class Product {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}