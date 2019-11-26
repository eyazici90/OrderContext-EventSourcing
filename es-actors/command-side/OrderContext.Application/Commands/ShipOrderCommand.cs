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
        public readonly string OrderNumber;

        public ShipOrderCommand(string orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
