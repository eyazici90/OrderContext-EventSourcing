using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderShippedEvent : INotification
    {
        public string OrderId { get; }
        public OrderShippedEvent(string orderId)
        {
            OrderId = orderId;
        }
    }
}
