using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderPaidEvent : INotification
    {
        public string OrderId { get; }
        public OrderPaidEvent(string orderId)
        {
            OrderId = orderId;
        }
    }
}
