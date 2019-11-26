using MediatR;
using OrderContext.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OrderContext.Application.Commands
{
    [DataContract]
    public class CreateOrderCommand : IRequest<OrderId>
    {
        [DataMember]
        public readonly string Id;
        [DataMember]
        public readonly string BuyerId;
        [DataMember]
        public readonly string City;
        [DataMember]
        public readonly string Street;
        public CreateOrderCommand(string id, string buyerId, 
            string city, string street)
        {
            Id = id;
            BuyerId = buyerId;
            City = city;
            Street = street;
        }
    }
}
