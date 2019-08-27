using ImGalaxy.ES.Core;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Messages.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public class OrderState : AggregateRootState<OrderState>
    {
        private OrderId _id;

        private AddresState _address;

        private CustomerId _buyerId;

        private DateTime _orderDate;

        private OrderStatus _orderStatus;

        private List<OrderItemState> _orderItems;

        public OrderId Id => _id;

        public OrderStatus OrderStatus => _orderStatus;

        private OrderState()
        {
            RegisterEvent<OrderStartedEvent>(When);
            RegisterEvent<OrderPaidEvent>(When);
            RegisterEvent<OrderShippedEvent>(When);
            RegisterEvent<OrderCancelledEvent>(When);
            RegisterEvent<OrderItemAddedEvent>(When);
        } 

        internal OrderState(OrderId id, CustomerId buyerId) : this() =>
            EnsureValidState(id, buyerId);

        private void EnsureValidState(OrderId id, CustomerId buyerId) =>
           this.ThrowsIf(_=> string.IsNullOrEmpty(id), new ArgumentNullException(id))
               .ThrowsIf(_=> string.IsNullOrEmpty(buyerId), new ArgumentNullException(buyerId));


        private void When(OrderStartedEvent @event) =>
           With(this, state =>
           {
               state._id = new OrderId(@event.OrderId);
               state._buyerId = new CustomerId(@event.BuyerId);
               state._address = Adress.Create(@event.Street, @event.City, string.Empty, string.Empty, string.Empty);
               state._orderStatus = OrderStatus.Submitted;
               state._orderDate = DateTime.Now;
           }); 

        private void When(OrderCancelledEvent @event) =>
           With(this, state =>
           {
               state._orderStatus = OrderStatus.Cancelled;
           });

        private void When(OrderShippedEvent @event) =>
           With(this, state =>
           {
               state._orderStatus = OrderStatus.Shipped;
           });

        private void When(OrderPaidEvent @event) =>
           With(this, state =>
           {
               state._orderStatus = OrderStatus.Paid;
           });

        private void When(OrderItemAddedEvent @event) =>
           With(this, state =>
           {

           });
    }
}
