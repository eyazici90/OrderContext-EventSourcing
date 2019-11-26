using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders.Snapshots
{
    public class OrderItemStateSnapshot
    {
        public string Id { get; set; }
        public string OrderId { get; set; }

        public string ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
    }
}
