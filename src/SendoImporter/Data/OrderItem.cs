using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SendoImporter.Data
{
    public class OrderItem
    {
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public int OrderId { get; set; }
        [NotMapped]
        public int ProductId { get; set; }
        [JsonProperty("product_variant_id")]
        public int ProductVariantId { get; set; }
        [JsonProperty("product_name")]
        public string ProductName { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
