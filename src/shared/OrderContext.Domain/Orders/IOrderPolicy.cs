using OrderContext.Domain.Shared; 

namespace OrderContext.Domain.Orders
{
    public interface IOrderPolicy 
        : IPolicy<OrderShouldBePaidBeforeShip>
    { 
    }
}
