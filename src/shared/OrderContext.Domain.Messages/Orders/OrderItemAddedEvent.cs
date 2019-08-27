using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderItemAddedEvent : INotification
    {
        public readonly string OrderItemId;
        public readonly string OrderId;

        public readonly string ProductId;

        public readonly decimal UnitPrice;

        public readonly decimal Discount;
        public OrderItemAddedEvent(string orderItemId, string orderId, string productId, 
            decimal unitPrice, decimal discount)
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Discount = discount;
        }
    }
}
