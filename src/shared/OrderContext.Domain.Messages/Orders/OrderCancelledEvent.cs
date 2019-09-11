using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderCancelledEvent : INotification
    {
        public string OrderId { get; }
        public OrderCancelledEvent(string orderId)
        {
            OrderId = orderId;
        }
    }
}
