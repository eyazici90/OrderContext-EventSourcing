using System.Collections.Generic;
using System.Threading.Tasks;
using ImGalaxy.ES.Core; 
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs; 
using OrderContext.Domain.Orders;
using OrderContext.Snapshotter.Functions.Extensions;

namespace OrderContext.Snapshotter
{
    public class SnapshotterFunctions
    {
        private readonly ISnapshotter _snapShotter;
        public SnapshotterFunctions(ISnapshotter snapShotter) =>
            _snapShotter = snapShotter;

        [FunctionName("Snapshotter")]
        public async Task Run([CosmosDBTrigger(
             databaseName: "OrderContextES",
             collectionName: "Events",
             ConnectionStringSetting = "ConStr",
             LeaseCollectionName = "leases-Snapshots",
             StartFromBeginning = true,
             CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input)
        {
            foreach (var @event in input)
            {
                var e = @event.ToEventDoc();

                var shouldWeTakeSnapshot = _snapShotter.ShouldTakeSnapshot(typeof(OrderState), e);

                if (shouldWeTakeSnapshot)
                    await _snapShotter.TakeSnapshotAsync(e.StreamId);
            }
        }
    }
}
