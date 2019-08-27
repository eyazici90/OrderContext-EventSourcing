using ImGalaxy.ES.Core;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Messages.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public static class Order
    { 
        public static OrderState.Result Create(OrderId id, CustomerId buyerId, string city, string street) =>
            new OrderState(id, buyerId).ApplyEvent(new OrderStartedEvent(id, buyerId, city, street));

        public static OrderState.Result ShipOrder(OrderState state) =>
            state.ThrowsIf(s => s.OrderStatus != OrderStatus.Paid, new OrderNotPaidYetException(state.Id))
                 .Then(s=> s.ApplyEvent(new OrderShippedEvent(state.Id)));

        public static OrderState.Result PayOrder(OrderState state) =>
           state.ApplyEvent(new OrderPaidEvent(state.Id));

        public static OrderState.Result CancelOrder(OrderState state) =>
           state.ApplyEvent(new OrderCancelledEvent(state.Id));
    }
}
