using MediatR; 
using System.Runtime.Serialization; 

namespace OrderContext.Domain.Messages.Orders
{
    public static class Commands
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

        [DataContract]
        public class CreateOrderCommand : IRequest<string>
        {
            [DataMember]
            public readonly string BuyerId;
            [DataMember]
            public readonly string City;
            [DataMember]
            public readonly string Street;
            public CreateOrderCommand(string buyerId,
                string city, string street)
            {
                BuyerId = buyerId;
                City = city;
                Street = street;
            }
        }
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
}
