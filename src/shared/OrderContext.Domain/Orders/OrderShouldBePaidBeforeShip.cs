using ImGalaxy.ES.Core;
using OrderContext.Domain.Shared;

namespace OrderContext.Domain.Orders
{
    public class OrderShouldBePaidBeforeShip  
    {
        public OrderState Order { get; }
        public OrderShouldBePaidBeforeShip(OrderState order)
        {
            Order = order;
        }
         
    }
}
