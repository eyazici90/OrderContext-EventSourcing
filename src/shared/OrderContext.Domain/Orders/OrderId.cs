using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{ 
    public class OrderId
    {
        public static OrderId New => new OrderId(Guid.NewGuid().ToString());

        public readonly string Id;
        public OrderId(string id) => Id = id;
        public override string ToString() => Id;

        public static implicit operator string(OrderId self) => self.Id;

        public static explicit operator OrderId(string value) => new OrderId(value);
    }
}
