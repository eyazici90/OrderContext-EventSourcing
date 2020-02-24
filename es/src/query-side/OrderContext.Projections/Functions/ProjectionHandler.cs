using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using OrderContext.Projections.Functions.Extensions;

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
                switch (@event.ToDynamic())
                {
                    case Domain.Messages.Orders.Events.OrderStartedEvent e:
                        await SendOrderStarted(e);
                        break;
                    case Domain.Messages.Orders.Events.OrderPaidEvent e:
                        await SendOrderPaid(e);
                        break;
                    case Domain.Messages.Orders.Events.OrderShippedEvent e:
                        await SendOrderShipped(e);
                        break;
                    case Domain.Messages.Orders.Events.OrderCancelledEvent e:
                        await SendOrderCancelled(e);
                        break;
                }
            }
        }

        private async Task SendOrderStarted(Domain.Messages.Orders.Events.OrderStartedEvent @event) =>
            await _client.WhenAsync(new OrderStartedEvent
            {
                BuyerId = @event.BuyerId,
                City = @event.City,
                OrderId = @event.OrderId,
                Street = @event.Street
            });

        private async Task SendOrderPaid(Domain.Messages.Orders.Events.OrderPaidEvent @event) =>
            await _client.WhenAsync(new OrderPaidEvent
            {
                OrderId = @event.OrderId
            });

        private async Task SendOrderShipped(Domain.Messages.Orders.Events.OrderShippedEvent @event) =>
            await _client.WhenAsync(new OrderShippedEvent
            {
                OrderId = @event.OrderId
            });

        private async Task SendOrderCancelled(Domain.Messages.Orders.Events.OrderCancelledEvent @event) =>
            await _client.WhenAsync(new OrderCancelledEvent
            {
                OrderId = @event.OrderId
            });

    }
}
