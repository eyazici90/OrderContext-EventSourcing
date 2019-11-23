using OrderContext.Domain.Messages.Orders;
using OrderContext.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public static class OrderItem
    { 
        public static OrderItemState.Result Create(OrderItemId id, OrderId orderId, ProductId productId, decimal unitPrice, decimal discount) =>
           new OrderItemState(id, productId, unitPrice, discount)
            .ApplyEvent(new OrderItemAddedEvent(id, orderId, productId, unitPrice, discount));
    }
}
