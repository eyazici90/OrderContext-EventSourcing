using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderContext.Integration.Events;
using OrderContext.Integration.Publisher.Application;
using OrderContext.Integration.Publisher.Functions.Extensions;
using static OrderContext.Domain.Messages.Orders.Events;

namespace OrderContext.Integration.Publisher
{
    public class IntegrationPublisher
    {
        private readonly IEventBus _eventBus;
        public IntegrationPublisher(IEventBus eventBus) =>
            _eventBus = eventBus;

        [FunctionName("IntegrationPublisher")]
        public async Task Run([CosmosDBTrigger(
            databaseName: ".",
            collectionName: ".",
            ConnectionStringSetting = ".",
            LeaseCollectionName = "leases-Integrations",
            StartFromBeginning = true,
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log)
        {
            foreach (var @event in input)
            {
                switch (@event.ToDynamic())
                {
                    case OrderStartedEvent e:
                        await _eventBus.PublishAsync(new IntegrationEvents.V1.OrderStartedEvent(e.OrderId));
                        break;
                }
            }
        }
    }
}
