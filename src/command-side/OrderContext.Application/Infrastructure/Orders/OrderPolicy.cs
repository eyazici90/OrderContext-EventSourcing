using ImGalaxy.ES.Core;
using OrderContext.Domain.Orders;

namespace OrderContext.Application.Infrastructure.Orders
{
    public class OrderPolicy : IOrderPolicy
    {
        public void Apply(OrderShouldBePaidBeforeShip policy) =>
           policy.ThrowsIf(p => p.Order.OrderStatus != OrderStatus.Paid,
                new OrderNotPaidYetException(policy.Order.Id));
    }
}
