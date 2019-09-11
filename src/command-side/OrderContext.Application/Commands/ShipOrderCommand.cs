using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OrderContext.Application.Commands
{
    [DataContract]
    public class ShipOrderCommand : IRequest
    {
        [DataMember]
        public string OrderNumber { get; }

        public ShipOrderCommand(string orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
