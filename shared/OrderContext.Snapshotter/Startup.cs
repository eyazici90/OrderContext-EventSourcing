using ImGalaxy.ES.Core;
using ImGalaxy.ES.CosmosDB;
using ImGalaxy.ES.CosmosDB.Modules;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Domain.Orders;
using OrderContext.Domain.Orders.Snapshots;

[assembly: FunctionsStartup(typeof(OrderContext.Snapshotter.Startup))]

namespace OrderContext.Snapshotter
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder) =>
           ConfigureServices(builder.Services);

        private void ConfigureServices(IServiceCollection services) =>
              services 
             .AddTransient<ISnapshotter, SnapshotterCosmosDB<OrderState, OrderStateSnapshot>>()
             .AddImGalaxyESCosmosDBModule(configs =>
             {
                 configs.DatabaseId = "OrderContextES";
                 configs.EventCollectionName = "Events";
                 configs.StreamCollectionName = "Streams";
                 configs.SnapshotCollectionName = "Snapshots";
                 configs.EndpointUri = "https://localhost:8081";
                 configs.PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                 configs.ReadBatchSize = 100;
                 configs.OfferThroughput = 10000;
                 configs.IsSnapshottingOn = true;
                 configs.SnapshotStrategy = @event => @event.Position != 0 && @event.Position % 5 == 0;
             });
    }
}
