using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using OrderContext.Snapshotter.Application;

namespace OrderContext.Snapshotter
{
    public static class Snapshotter
    {
        [FunctionName("Snapshotter")]
        public static async Task Run([CosmosDBTrigger(
             databaseName: ".",
             collectionName: ".",
             ConnectionStringSetting = ".",
             LeaseCollectionName = "leases-Snapshots",
             StartFromBeginning = true,
             CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log)
        {
            await SnapshotInstanceService.InitSnapshots(input);
        }


    }
}
