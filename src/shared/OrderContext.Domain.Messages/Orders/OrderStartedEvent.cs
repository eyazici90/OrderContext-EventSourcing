using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Domain.Messages.Orders
{
    public class OrderStartedEvent: INotification
    {
        public readonly string OrderId;
        public readonly string BuyerId;
        public readonly string City;
        public readonly string Street;
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
