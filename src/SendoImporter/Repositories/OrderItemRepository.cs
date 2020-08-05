using MySql.Data.MySqlClient;
using SendoImporter.Data;
using System.Data;

namespace SendoImporter.Repositories
{
    public class OrderItemRepository:IOrderItemRepository
    {
        public bool Insert(OrderItem item)
        {
            string sql = "INSERT INTO sendo_order_items (order_id, product_id, product_variant_id, product_name, quantity, price) VALUES (@order_id, @product_id, @product_variant_id, @product_name, @quantity, @price)";
            using (var cmd = new MySqlCommand(sql, new MySqlConnection(Config.ConnectionString)))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@order_id", item.OrderId);
                cmd.Parameters.AddWithValue("@product_id", item.ProductId);
                cmd.Parameters.AddWithValue("@product_variant_id", item.ProductVariantId);
                cmd.Parameters.AddWithValue("@product_name", item.ProductName);
                cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@price", item.Price);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    return true;

            }
            return false;
        }
    }
}
