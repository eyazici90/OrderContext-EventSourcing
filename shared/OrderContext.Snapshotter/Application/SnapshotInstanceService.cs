using ImGalaxy.ES.Core;
using ImGalaxy.ES.CosmosDB.Documents;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using OrderContext.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderContext.Snapshotter.Application
{
    public class SnapshotInstanceService
    {

        public static async Task InitSnapshots(IReadOnlyList<Document> input)
        {
             
        }
        private static async Task HandleSnapshots<TAggregateRoot, TSnapshot>(IServiceProvider provider, EventDocument @event)
          where TAggregateRoot : IAggregateRoot, ISnapshotable
        {

            var snapshotter = ImGalaxyESCosmosDBModule.GetSnapshotter<TAggregateRoot, TSnapshot>(provider);

            var shouldWeTakeSnapshot = snapshotter.ShouldTakeSnapshot(typeof(TAggregateRoot),
                   @event);

            if (shouldWeTakeSnapshot)
                await snapshotter.TakeSnapshotAsync(@event.StreamId);
        }
    }
}
