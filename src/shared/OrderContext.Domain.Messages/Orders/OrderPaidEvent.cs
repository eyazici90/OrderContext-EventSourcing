using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderPaidEvent : INotification
    {
        public readonly string OrderId;
        public OrderPaidEvent(string orderId)
        {
            OrderId = orderId;
        }
    }
}
