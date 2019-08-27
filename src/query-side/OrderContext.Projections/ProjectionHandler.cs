using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderContext.Domain.Messages.Orders; 
using OrderContextQueryClient;
using OrderCancelledEvent = OrderContext.Domain.Messages.Orders.OrderCancelledEvent;
using OrderPaidEvent = OrderContext.Domain.Messages.Orders.OrderPaidEvent;
using OrderShippedEvent = OrderContext.Domain.Messages.Orders.OrderShippedEvent;
using OrderStartedEvent = OrderContext.Domain.Messages.Orders.OrderStartedEvent;

namespace OrderContext.Projections
{
    public static class ProjectionHandler
    {
        [FunctionName("ProjectionHandler")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: ".",
            collectionName: ".",
            ConnectionStringSetting = ".",
            LeaseCollectionName = "leases-projection",
            StartFromBeginning = true,
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log)
        {
            foreach (var @event in input)
            {
                switch (CastEventToDynamic(@event))
                {
                    case OrderStartedEvent e:
                        await SendOrderStarted(e);
                        break;
                    case OrderPaidEvent e:
                        await SendOrderPaid(e);
                        break;
                    case OrderShippedEvent e:
                        await SendOrderShipped(e);
                        break;
                    case OrderCancelledEvent e:
                        await SendOrderCancelled(e);
                        break;
                }
            }
        }

        private static async Task SendOrderStarted(OrderStartedEvent @event)=>
            await OrderContextHttpClient.WhenAsync(new OrderContextQueryClient.OrderStartedEvent
            {
                BuyerId = @event.BuyerId,
                City = @event.City,
                OrderId = @event.OrderId,
                Street = @event.Street
            });
        

        private static async Task SendOrderPaid(OrderPaidEvent @event)=>
            await OrderContextHttpClient.WhenAsync(new OrderContextQueryClient.OrderPaidEvent
            {
                OrderId = @event.OrderId
            }); 
        

        private static async Task SendOrderShipped(OrderShippedEvent @event)=> 
            await OrderContextHttpClient.WhenAsync(new OrderContextQueryClient.OrderShippedEvent
            {
                OrderId = @event.OrderId
            });
        
        private static async Task SendOrderCancelled(OrderCancelledEvent @event)=>
            await OrderContextHttpClient.WhenAsync(new OrderContextQueryClient.OrderCancelledEvent
            {
                OrderId = @event.OrderId
            });
        

        private static Client OrderContextHttpClient => new Client("http://localhost:5001", HttpClientFactory.Create());
        private static dynamic CastEventToDynamic(Document @event)
        {
            var eData = ((dynamic)@event).Data;
            var eType = ((dynamic)@event).Type;

            var type = Convert.ToString(eType);

            var castedEvent = JsonConvert.DeserializeObject(eData, Type.GetType(type));

            return castedEvent;
        }
    }
}
