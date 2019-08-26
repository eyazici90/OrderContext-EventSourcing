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
        public readonly string BuyerId;
        [DataMember]
        public readonly string City;
        [DataMember]
        public readonly string Street;
        public CreateOrderCommand(string buyerId, string city, string street)
        {
            BuyerId = buyerId;
            City = city;
            Street = street;
        }
    }
}
