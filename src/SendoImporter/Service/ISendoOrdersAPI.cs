using SendoImporter.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SendoImporter.Service
{
    public interface ISendoOrdersAPI
    {
        Task<IList<Order>> GetOrders(string token, DateTime from, DateTime to);
    }
}
