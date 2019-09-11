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
        public string OrderId { get; }
        [DataMember]
        public string BuyerId { get; }
        [DataMember]
        public string City { get; }
        [DataMember]
        public string Street { get; }
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
