using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendoImporter.Data
{
    public interface IOrderRepository : IRepository<Order>
    {
        int Insert(Order item);

        bool Update(Order item);

        int GetOrderIdByOrderNo(string orderNo);
    }
}
