﻿using ImGalaxy.ES.Core; 

namespace OrderContext.Domain.Orders
{
    public class OrderPolicy : IOrderPolicy
    {
        public IExecutionResult Apply(OrderShouldBePaidBeforeShip policy) 
        {
            policy.ThrowsIf(p => p.Order.OrderStatus != OrderStatus.Paid,
                   new OrderNotPaidYetException(policy.Order.Id));

            return ExecutionResult.Success;
        } 
    }
}
