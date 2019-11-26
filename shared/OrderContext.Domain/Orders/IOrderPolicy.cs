using ImGalaxy.ES.Core;

namespace OrderContext.Domain.Orders
{
    public interface IOrderPolicy 
        : IPolicy<OrderShouldBePaidBeforeShip>
    { 
    }
}
