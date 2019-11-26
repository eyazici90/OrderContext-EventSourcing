using ImGalaxy.ES.Core;
using System; 

namespace OrderContext.Domain.Orders
{ 
    public class OrderId : Identity<string>
    {
        public static OrderId New => new OrderId(Guid.NewGuid().ToString());
        public OrderId(string id) : base(id)
        {
        } 

        public static implicit operator string(OrderId self) => self.Id;

        public static explicit operator OrderId(string value) => new OrderId(value);
    }
}
