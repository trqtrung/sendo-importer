using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SendoImporter.Data
{
    public class Order
    {
        [NotMapped]
        public int Id { get; set; }
        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }
        [JsonProperty("order_status")]
        public int OrderStatus { get; set; }
        [JsonProperty("payment_status")]
        public int PaymentStatus { get; set; }
        [JsonProperty("total_amount")]
        public double TotalAmount { get; set; }
        [JsonProperty("total_amount_buyer")]
        public double TotalAmountBuyer { get; set; }
        [JsonProperty("sub_total")]
        public double SubTotal { get; set; }
        [JsonProperty("buyer_phone")]
        public string BuyerPhone { get; set; }
        [JsonProperty("buyer_address")]
        public string BuyerAddress { get; set; }
        [JsonProperty("ship_to_address")]
        public string ShipToAddress { get; set; }
        [JsonProperty("shipping_contact_phone")]
        public string ShippingContactPhone { get; set; }
        [JsonProperty("receiver_name")]
        public string ReceiverName { get; set; }
        [JsonProperty("order_date_time_stamp")]
        public int OrderDateTimeStamp { get; set; }
        [NotMapped]
        public DateTime Created { get; set; }
        [NotMapped]
        public DateTime Updated { get; set; }

        [JsonProperty("sku_details")]
        public List<OrderItem> Items { get; set; }
    }
}
