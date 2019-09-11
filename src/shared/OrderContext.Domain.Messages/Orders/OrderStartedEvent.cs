using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderStartedEvent: INotification
    {
        public string OrderId { get; }
        public string BuyerId { get; }
        public string City { get; }
        public string Street { get; }
        public OrderStartedEvent(string orderId, string buyerId, 
            string city, string street)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            City = city;
            Street = street;
        }
    }
}
