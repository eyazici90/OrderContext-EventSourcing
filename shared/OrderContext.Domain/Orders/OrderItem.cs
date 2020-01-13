using OrderContext.Domain.Products; 
using static OrderContext.Domain.Messages.Orders.Events;

namespace OrderContext.Domain.Orders
{
    public static class OrderItem
    { 
        public static OrderItemState.Result Create(OrderItemId id, OrderId orderId, ProductId productId, decimal unitPrice, decimal discount) =>
           new OrderItemState(id, productId, unitPrice, discount)
            .ApplyEvent(new OrderItemAddedEvent(id, orderId, productId, unitPrice, discount));
    }
}
