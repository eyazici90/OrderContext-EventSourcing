using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OrderContext.Application.Commands
{
    [DataContract]
    public class CancelOrderCommand : IRequest
    {
        [DataMember]
        public readonly string OrderNumber;

        public CancelOrderCommand(string orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
