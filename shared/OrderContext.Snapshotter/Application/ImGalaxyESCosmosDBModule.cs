using ImGalaxy.ES.Core;
using ImGalaxy.ES.CosmosDB;
using ImGalaxy.ES.CosmosDB.Modules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Snapshotter.Application
{
    public static class ImGalaxyESCosmosDBModule
    {
        public static IServiceProvider Initialize()
        {
            var services = new ServiceCollection();
            services
             .AddMediatR(typeof(ImGalaxyESCosmosDBModule).Assembly)
             .AddImGalaxyESCosmosDBModule(configs =>
             {
                 configs.DatabaseId = ".";
                 configs.EventCollectionName = "Events";
                 configs.StreamCollectionName = "Streams";
                 configs.SnapshotCollectionName = "Snapshots";
                 configs.EndpointUri = ".";
                 configs.PrimaryKey = ".";
                 configs.ReadBatchSize = 100;
                 configs.OfferThroughput = 10000;
                 configs.IsSnapshottingOn = true;
                 configs.SnapshotStrategy = @event => @event.Position != 0 && @event.Position % 25 == 0;
             });
            return services.BuildServiceProvider()
                           .UseGalaxyESCosmosDBModule()
                           .ConfigureAwait(false)
                           .GetAwaiter()
                           .GetResult();
        }

        public static ISnapshotter GetSnapshotter<TAggregateRoot, TSnapshot>(IServiceProvider provider)
             where TAggregateRoot : class, IAggregateRootState<TAggregateRoot>, IAggregateRoot, ISnapshotable
        {
            var scope = provider.CreateScope();
            var configurations = scope.ServiceProvider.GetRequiredService<ICosmosDBConfigurations>();
            var cosmosClient = scope.ServiceProvider.GetRequiredService<ICosmosDBClient>();

            var alterationRootRep = scope.ServiceProvider.GetRequiredService<IAggregateRootRepository<TAggregateRoot>>();

            var changeTracker = scope.ServiceProvider.GetRequiredService<IChangeTracker>();
            var serializer = scope.ServiceProvider.GetRequiredService<IEventSerializer>();

            return new SnapshotterCosmosDB<TAggregateRoot, TSnapshot>(alterationRootRep, changeTracker, cosmosClient, configurations,
               serializer);
        }
    }
}
