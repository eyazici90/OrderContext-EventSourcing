using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OrderContext.Application.Commands
{ 
    [DataContract]
    public class PayOrderCommand : IRequest
    {
        [DataMember]
        public readonly string OrderNumber;

        public PayOrderCommand(string orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
