using System.Collections.Generic;
using System.Threading.Tasks;
using ImGalaxy.ES.Projector;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using OrderContext.Projections.Extensions;  
using OrderContext.Query.Shared;

namespace OrderContext.Projections
{
    public class ProjectionFunctions
    {
        private readonly IProjector _projector;
        public ProjectionFunctions(IProjector projector) =>
            _projector = projector;

        [FunctionName("ProjectionFunctions")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "OrderContextES",
            collectionName: "Events",
            ConnectionStringSetting = "ConnString",
            LeaseDatabaseName = SettingConsts.DATABASE,
            LeaseCollectionName = "leases-projection",
            StartFromBeginning = true,
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> changes) =>
                await changes.ForEachAsync(async change =>
                {
                    var @event = (object)change.DeserializeToEvent();
                    await _projector.ProjectAsync(@event).ConfigureAwait(false);
                });

    }
}
