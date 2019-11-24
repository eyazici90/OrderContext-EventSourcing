using ImGalaxy.ES.Core;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Messages.Orders;
using OrderContext.Domain.Products;
using OrderContext.Domain.Shared;

namespace OrderContext.Domain.Orders
{
    public static class Order
    {
        public static OrderState.Result Create(OrderId id, CustomerId buyerId, string city, string street) =>
            new OrderState(id, buyerId).ApplyEvent(new OrderStartedEvent(id, buyerId, city, street));

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
