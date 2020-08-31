using Galaxy.Railway; 

namespace OrderContext.Domain.Orders
{
    public class OrderPolicy : IOrderPolicy
    {
        public IExecutionResult Apply(OrderShouldBePaidBeforeShip policy) =>
            policy.ThrowsIf(p => p.Order.OrderStatus != OrderStatus.Paid, new OrderNotPaidYetException(policy.Order.Id))
                   .Map(_ => ExecutionResult.Success);

    }
}
