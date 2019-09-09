using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OrderContext.Application.Commands
{
    [DataContract]
    public class CreateOrderCommand : IRequest
    {
        [DataMember]
        public readonly string OrderId;
        [DataMember]
        public readonly string BuyerId;
        [DataMember]
        public readonly string City;
        [DataMember]
        public readonly string Street;
        public CreateOrderCommand(string orderId, string buyerId, 
            string city, string street)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            City = city;
            Street = street;
        }
    }
}
