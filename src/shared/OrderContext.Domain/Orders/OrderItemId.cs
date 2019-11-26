using ImGalaxy.ES.Core;
using System; 

namespace OrderContext.Domain.Orders
{ 
    public class OrderItemId : Identity<string>
    {
        public static OrderItemId New => new OrderItemId(Guid.NewGuid().ToString());
        public OrderItemId(string id) : base(id)
        {
        } 

        public static implicit operator string(OrderItemId self) => self.Id;

        public static explicit operator OrderItemId(string value) => new OrderItemId(value);
    }
}
