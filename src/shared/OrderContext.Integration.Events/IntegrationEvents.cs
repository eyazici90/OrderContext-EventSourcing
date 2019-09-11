using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Integration.Events
{
    public static class IntegrationEvents
    {
        public static class V1
        {
            public class OrderStartedEvent 
            {
                public string OrderId { get; }
                public OrderStartedEvent(string orderId)
                {
                    OrderId = orderId;
                }
            }
        }
    }
}
