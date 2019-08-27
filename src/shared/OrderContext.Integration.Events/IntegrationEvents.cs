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
                public readonly string OrderId; 
                public OrderStartedEvent(string orderId)
                {
                    OrderId = orderId;
                }
            }
        }
    }
}
