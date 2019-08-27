using ImGalaxy.ES.Core;
using OrderContext.Domain.Messages.Orders;
using OrderContext.Domain.Orders.Snapshots;
using OrderContext.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Orders
{
    public class OrderItemState : EntityState<OrderItemState>, ISnapshotable
    {
        private OrderItemId _id;

        private OrderId _orderId;

        private ProductId _productId;

        private decimal _unitPrice;

        private decimal _discount;

        private OrderItemState()
        {
            RegisterEvent<OrderItemAddedEvent>(When);
        }
         
        internal OrderItemState(OrderItemId id, ProductId productId, decimal unitPrice, decimal discount) =>
            EnsureValidState(id, productId, unitPrice, discount);

        private void EnsureValidState(OrderItemId id, ProductId productId, decimal unitPrice, decimal discount) =>
            this.ThrowsIf(_=> string.IsNullOrEmpty(id), new ArgumentNullException(id))
                .ThrowsIf(_=> string.IsNullOrEmpty(productId), new ArgumentNullException(productId))
                .ThrowsIf(_=> unitPrice < 0, new InvalidValueException(unitPrice.ToString()))
                .ThrowsIf(_=> discount < 0, new InvalidValueException(discount.ToString()));


        private void When(OrderItemAddedEvent @event) =>
            With(this, state=> 
            {
                state._id = new OrderItemId(@event.OrderItemId);
                state._orderId = new OrderId(@event.OrderId);
                state._productId = new ProductId(@event.ProductId);
                state._unitPrice = @event.UnitPrice;
                state._discount = @event.Discount;
            });


        public void RestoreSnapshot(object stateSnapshot) =>
            With(this, state =>
            {
                var snapshot = (OrderItemStateSnapshot)stateSnapshot;
                state._id = new OrderItemId(snapshot.Id);
                state._orderId = new OrderId(snapshot.OrderId);
                state._productId = new ProductId(snapshot.ProductId);
                state._unitPrice = snapshot.UnitPrice;
                state._discount = snapshot.Discount;
            });

        public object TakeSnapshot() =>
            new OrderItemStateSnapshot
            {
                Id = this._id,
                OrderId = this._orderId,
                ProductId = this._productId,
                Discount = this._discount,
                UnitPrice = this._unitPrice
            };
    }
}
