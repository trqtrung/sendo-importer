using Newtonsoft.Json.Linq;
using SendoImporter.Data;

namespace SendoImporter.Service
{
    public class OrderingService
    {
        public Order MapOrder(JObject jObject)
        {
            Order order = jObject.ToObject<Order>();

            return order;
        }

        //public OrderItem MapOrderItem(JObject jObject)
    }
}
