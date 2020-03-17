using System.Collections.Generic;
using System.Threading.Tasks;
using ImGalaxy.ES.Projector;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using OrderContext.Query.Function.Host;
using OrderContext.Query.Function.Host.Extensions;
using OrderContext.Query.Function.Host.Model;

namespace OrderContext.Projections
{
    public class ProjectionFunctions
    {
        private readonly IProjector _projector;
        public ProjectionFunctions(IProjector projector) =>
            _projector = projector;

        [FunctionName("ProjectionHandler")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "OrderContextES",
            collectionName: "Events",
            ConnectionStringSetting = "ConStr",
            LeaseDatabaseName = SettingConsts.DATABASE,
            LeaseCollectionName = "leases-projection",
            StartFromBeginning = true,
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input) =>
               await input.ForEachAsync(async doc =>
               {
                   var @event = doc.ToDynamic();
                   await _projector.ProjectAsync<Order>(((dynamic)doc).StreamId, @event);
               });

    }
}
