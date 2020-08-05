using MySql.Data.MySqlClient;
using SendoImporter.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SendoImporter.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public int Insert(Order item)
        {
            string sql = "INSERT INTO sendo_orders (order_number, order_status, payment_status, total_amount, total_amount_buyer, sub_total,buyer_phone,buyer_address,ship_to_address,shipping_contact_phone,receiver_name,order_date_time_stamp,created) VALUES (@order_number, @order_status, @payment_status, @total_amount, @total_amount_buyer, @sub_total,@buyer_phone,@buyer_address,@ship_to_address,@shipping_contact_phone,@receiver_name,@order_date_time_stamp,@created); SELECT LAST_INSERT_ID();";
            using (var cmd = new MySqlCommand(sql, new MySqlConnection(Config.ConnectionString)))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@order_number", item.OrderNumber);
                cmd.Parameters.AddWithValue("@order_status", item.OrderStatus);
                cmd.Parameters.AddWithValue("@payment_status", item.PaymentStatus);
                cmd.Parameters.AddWithValue("@total_amount", item.TotalAmount);
                cmd.Parameters.AddWithValue("@total_amount_buyer", item.TotalAmountBuyer);
                cmd.Parameters.AddWithValue("@sub_total", item.SubTotal);
                cmd.Parameters.AddWithValue("@buyer_phone", item.BuyerPhone);
                cmd.Parameters.AddWithValue("@buyer_address", item.BuyerAddress.ToString());
                cmd.Parameters.AddWithValue("@ship_to_address", item.ShipToAddress.ToString());
                cmd.Parameters.AddWithValue("@shipping_contact_phone", item.ShippingContactPhone);
                cmd.Parameters.AddWithValue("@receiver_name", item.ReceiverName.ToString());
                cmd.Parameters.AddWithValue("@order_date_time_stamp", item.OrderDateTimeStamp);
                cmd.Parameters.AddWithValue("@created", DateTime.Now);

                int result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;

            }
        }

        public bool Update(Order item)
        {
            string sql = "UPDATE sendo_orders SET order_status=@order_status, payment_status=@payment_status, updated=@updated WHERE id=@id AND order_number=@order_number";
            using (var cmd = new MySqlCommand(sql, new MySqlConnection(Config.ConnectionString)))
            {
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@order_number", item.OrderNumber);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@order_status", item.OrderStatus);
                cmd.Parameters.AddWithValue("@payment_status", item.PaymentStatus);
                cmd.Parameters.AddWithValue("@updated", DateTime.Now);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    return true;

            }
            return false;
        }
        public int GetOrderIdByOrderNo(string orderNo)
        {
            string sql = "SELECT id FROM sendo_orders WHERE order_number=@order_number";
            using (var cmd = new MySqlCommand(sql, new MySqlConnection(Config.ConnectionString)))
            {
                cmd.Connection.Open();

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@order_number", orderNo);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["id"] != DBNull.Value)
                                return Convert.ToInt32(reader["id"]);
                        }
                    }
                }
            }
            return 0;
        }
    }
}
