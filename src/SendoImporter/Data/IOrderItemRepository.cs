using System;
using System.Collections.Generic;
using System.Text;

namespace SendoImporter.Data
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        bool Insert(OrderItem item);
    }
}
