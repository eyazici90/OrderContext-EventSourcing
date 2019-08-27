using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders.Snapshots
{
    public class OrderStateSnapshot
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public IEnumerable<OrderItemStateSnapshot> OrderItems { get; set; }
    }
}
