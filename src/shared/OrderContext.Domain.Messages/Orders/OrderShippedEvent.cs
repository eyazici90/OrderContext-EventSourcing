using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderShippedEvent : INotification
    {
        public readonly string OrderId;
        public OrderShippedEvent(string orderId)
        {
            OrderId = orderId;
        }
    }
}
