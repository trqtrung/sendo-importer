using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendoImporter.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SendoImporter.Service
{
    public class SendoAPI : ISendoTokenAPI, ISendoOrdersAPI
    {
        public async Task<string> GetToken()
        {
            string url = "https://open.sendo.vn/login";
            string shopKey = Config.ShopID;
            string secretKey = Config.SecretKey;

            var json = new { shop_key = shopKey, secret_key = secretKey };
            var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject data = JObject.Parse(responseString);

                var result = data.GetValue("result") as JObject;

                return result.GetValue("token").ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<IList<Order>> GetOrders(string token, DateTime from, DateTime to)
        {
            var json = new { from = from, to = to};

            string url = "https://open.sendo.vn/api/partner/salesorder/search";

            var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();

            IList<Order> orders = new List<Order>();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject data = JObject.Parse(responseString);

                var result = data.GetValue("result") as JObject;

                var list = result.GetValue("data").ToObject<List<dynamic>>(); 
                
                foreach(JObject item in list)
                {
                    Order o = item.GetValue("sales_order").ToObject<Order>();
                    List<OrderItem> items = item.GetValue("sku_details").ToObject<List<OrderItem>>();
                    o.Items = items;
                    orders.Add(o);
                }
            }

            return orders;
        }
    }
}
