using ImGalaxy.ES.Core;
using OrderContext.Domain.Messages.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public class OrderItemState : EntityState<OrderItemState>
    {
        private OrderItemId _id;

        private OrderItemState()
        {
            RegisterEvent<OrderItemAddedEvent>(When);
        }
         
        internal OrderItemState(OrderItemId id) =>
            EnsureValidState();

        private void EnsureValidState() =>
             _id.ThrowsIfNull(new ArgumentNullException(_id));


        private void When(OrderItemAddedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
