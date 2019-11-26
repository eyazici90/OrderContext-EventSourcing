using ImGalaxy.ES.Core; 

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
