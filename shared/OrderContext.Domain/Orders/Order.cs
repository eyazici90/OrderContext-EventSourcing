using ImGalaxy.ES.Core;
using OrderContext.Domain.Customers; 
using OrderContext.Domain.Products;
using OrderContext.Domain.Shared;
using static OrderContext.Domain.Messages.Orders.Events;

namespace OrderContext.Domain.Orders
{
    public static class Order
    {
        public static OrderState.Result Create(OrderId id, 
            CustomerId buyerId, 
            string city, 
            string street,
            Now now) =>
            new OrderState(id, buyerId).ApplyEvent(new OrderStartedEvent(id, buyerId, city, street, now()));

        public static OrderState.Result ShipOrder(OrderState state, IOrderPolicy policy) =>
            state.With(s => policy.Apply(new OrderShouldBePaidBeforeShip(s)))
                 .Then(s => s.ApplyEvent(new OrderShippedEvent(s.Id)));

        public static OrderState.Result PayOrder(OrderState state) =>
            state.ApplyEvent(new OrderPaidEvent(state.Id));

        public static OrderState.Result CancelOrder(OrderState state) =>
            state.ApplyEvent(new OrderCancelledEvent(state.Id));

        public static OrderState.Result AddOrderItem(OrderState state, OrderItemId itemId, ProductId productId,
                    decimal unitPrice, decimal discount) =>
           state.ApplyEvent(new OrderItemAddedEvent(itemId, state.Id, productId, unitPrice, discount));
    }
}
