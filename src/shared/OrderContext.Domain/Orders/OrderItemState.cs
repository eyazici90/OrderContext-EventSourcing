using ImGalaxy.ES.Core;
using OrderContext.Domain.Messages.Orders;
using OrderContext.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public class OrderItemState : EntityState<OrderItemState>
    {
        private OrderItemId _id;

        private ProductId _productId;

        private decimal _unitPrice;

        private decimal _discount;

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
