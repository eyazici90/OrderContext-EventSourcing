using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{ 
    public class OrderItemId
    {
        public static OrderId New => new OrderId(Guid.NewGuid().ToString());

        public readonly string Id;
        public OrderItemId(string id) => Id = id;
        public override string ToString() => Id;

        public static implicit operator string(OrderItemId self) => self.Id;

        public static explicit operator OrderItemId(string value) => new OrderItemId(value);
    }
}
