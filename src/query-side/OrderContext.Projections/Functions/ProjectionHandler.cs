using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json; 

namespace OrderContext.Projections
{
    public class ProjectionHandler
    {
        private readonly OrderContextQueryClient _client;
        public ProjectionHandler(OrderContextQueryClient client) =>
            _client = client;

        [FunctionName("ProjectionHandler")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "OrderContextES",
            collectionName: "Events",
            ConnectionStringSetting = "ConStr",
            LeaseCollectionName = "leases-projection",
            StartFromBeginning = true,
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log)
        {
            foreach (var @event in input)
            {
                switch (CastEventToDynamic(@event))
                {
                    case OrderContext.Domain.Messages.Orders.OrderStartedEvent e:
                        await SendOrderStarted(e);
                        break;
                    case Domain.Messages.Orders.OrderPaidEvent e:
                        await SendOrderPaid(e);
                        break;
                    case Domain.Messages.Orders.OrderShippedEvent e:
                        await SendOrderShipped(e);
                        break;
                    case Domain.Messages.Orders.OrderCancelledEvent e:
                        await SendOrderCancelled(e);
                        break;
                }
            }
        }

        private async Task SendOrderStarted(Domain.Messages.Orders.OrderStartedEvent @event)=>
            await _client.WhenAsync(new OrderStartedEvent
            {
                BuyerId = @event.BuyerId,
                City = @event.City,
                OrderId = @event.OrderId,
                Street = @event.Street
            });
        

        private async Task SendOrderPaid(Domain.Messages.Orders.OrderPaidEvent @event)=>
            await _client.WhenAsync(new OrderPaidEvent
            {
                OrderId = @event.OrderId
            }); 
        

        private async Task SendOrderShipped(Domain.Messages.Orders.OrderShippedEvent @event)=> 
            await _client.WhenAsync(new OrderShippedEvent
            {
                OrderId = @event.OrderId
            });
        
        private async Task SendOrderCancelled(Domain.Messages.Orders.OrderCancelledEvent @event)=>
            await _client.WhenAsync(new OrderCancelledEvent
            {
                OrderId = @event.OrderId
            });
        
         
        private dynamic CastEventToDynamic(Document @event)
        {
            var eData = ((dynamic)@event).Data;
            var eType = ((dynamic)@event).Type;

            var type = Convert.ToString(eType);

            var castedEvent = JsonConvert.DeserializeObject(eData, Type.GetType(type));

            return castedEvent;
        }
    }
}
