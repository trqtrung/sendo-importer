using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendoImporter.ColectionData;
using SendoImporter.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SendoImporter.Service
{
    public class SendoApiRefractory : ICollectionService<IEnumerable<Order>>, ICollectionCommand<IEnumerable<Order>>
    {
        private IEnumerable<Order> _result;
        private const string URL = "https://open.sendo.vn/login";
        private const string URL_GET_DATA = "https://open.sendo.vn/api/partner/salesorder/search";
        public DateTime From;
        public DateTime To;
        private Action<SendoApiRefractory> MappingParams;

        public SendoApiRefractory(Action<SendoApiRefractory> mappingParams)
        {
            _result = new List<Order>();
            MappingParams = mappingParams;
        }
        public IEnumerable<Order> Result => _result;

        public async Task<string> GetToken()
        {
            
            string shopKey = Config.ShopID;
            string secretKey = Config.SecretKey;

            var json = new { shop_key = shopKey, secret_key = secretKey };
            var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(URL, content);

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

        public async Task<IEnumerable<Order>> Collect(string token, DateTime from, DateTime to)
        {
            var json = new { from = from, to = to };

            var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync(URL_GET_DATA, content);

            var responseString = await response.Content.ReadAsStringAsync();

            IList<Order> orders = new List<Order>();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject data = JObject.Parse(responseString);

                var result = data.GetValue("result") as JObject;

                var list = result.GetValue("data").ToObject<List<dynamic>>();

                foreach (JObject item in list)
                {
                    Order o = item.GetValue("sales_order").ToObject<Order>();
                    List<OrderItem> items = item.GetValue("sku_details").ToObject<List<OrderItem>>();
                    o.Items = items;
                    orders.Add(o);
                }
            }

            return orders;
        }

        public async Task Execute()
        {
            string token = await GetToken();
            _result = await Collect(token, From, To);
        }

    }

    public class Params
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
