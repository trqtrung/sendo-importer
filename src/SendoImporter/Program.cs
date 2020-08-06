using SendoImporter.ColectionData;
using SendoImporter.Data;
using SendoImporter.Repositories;
using SendoImporter.Service;
using System;
using System.Collections.Generic;

namespace SendoImporter
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Sendo Importer - .Net Core Console App with SOLID principles");

            CollectionServiceBase<List<Order>> sendoAPI = new SendoApiRefractory<List<Order>>();
            sendoAPI.MappingParams(a =>
            {
                a.From = DateTime.Today.AddDays(-14);
                a.To = DateTime.Now;
            });
            //SendoAPI sendoAPI = new SendoAPI();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJTdG9yZUlkIjoiMzgyMDA5IiwiVXNlck5hbWUiOiIiLCJTdG9yZVN0YXR1cyI6IjIiLCJTaG9wVHlwZSI6IjAiLCJTdG9yZUxldmVsIjoiMCIsImV4cCI6MTU5NjY0OTk4MSwiaXNzIjoiMzgyMDA5IiwiYXVkIjoiMzgyMDA5In0.Bd1AqHjcLlYTXFkInlHbTY2vP_XTmQ40pQ_agk-1hjQ"; //await sendoAPI.GetToken();

            if (!string.IsNullOrEmpty(token))
            {
                await sendoAPI.Execute();
                IList<Order> orders = sendoAPI.Result;

                if (orders.Count > 0)
                {
                    OrderRepository orderRepo = new OrderRepository();
                    OrderItemRepository orderItemRepo = new OrderItemRepository();

                    foreach (Order order in orders)
                    {
                        int orderId = orderRepo.GetOrderIdByOrderNo(order.OrderNumber);

                        if (orderId > 0)
                        {
                            //update
                            if (orderRepo.Update(order))
                            {
                                Console.WriteLine("Updated order {0}", order.OrderNumber);
                            }
                            continue;
                        }
                        else
                        {
                            //insert new order
                            orderId = orderRepo.Insert(order);
                            if (orderId > 0)
                            {
                                Console.WriteLine("Inserted order {0}", order.OrderNumber);

                                //insert items
                                foreach (OrderItem orderItem in order.Items)
                                {
                                    orderItem.OrderId = orderId;
                                    orderItemRepo.Insert(orderItem);
                                }
                            }
                            else
                            {
                                //insert failed
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No new orders found");
                }
            }
            else
            {
                Console.WriteLine("Can not get token");
            }
        }
    }
}
