using ImGalaxy.ES.Core;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Messages.Orders;
using OrderContext.Domain.Orders.Snapshots;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public class OrderState : AggregateRootState<OrderState>, ISnapshotable
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
               state._address = Address.Create(@event.Street, @event.City, string.Empty, string.Empty, string.Empty);
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

        public void RestoreSnapshot(object stateSnapshot) =>
            With(this, state=> 
            {
                var snapshot = (OrderStateSnapshot)stateSnapshot;
                state._id = new OrderId(snapshot.Id);
                state._buyerId = new CustomerId(snapshot.BuyerId);
                state._orderDate = snapshot.OrderDate;
                state._address = state._address ?? Address.Create(snapshot.Street, snapshot.City, string.Empty, string.Empty, string.Empty);
            });

        public object TakeSnapshot()=>
            new OrderStateSnapshot
            {
                Id = this._id,
                BuyerId = this._buyerId,
                City = this._address.City,
                OrderDate = this._orderDate,
                Street = this._address.Street,
                OrderStatus = this._orderStatus.Name
            };
    }
}
