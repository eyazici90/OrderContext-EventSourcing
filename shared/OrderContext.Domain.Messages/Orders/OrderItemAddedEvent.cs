using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderItemAddedEvent : INotification
    {
        public string OrderItemId { get; }
        public string OrderId { get; }
        public string ProductId { get; }
        public decimal UnitPrice { get; }
        public decimal Discount { get; }
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
