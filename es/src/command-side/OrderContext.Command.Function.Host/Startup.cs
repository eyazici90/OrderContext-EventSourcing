using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Command.Function.Host;
using OrderContext.Domain.Orders;
using OrderContext.Domain.Shared;

[assembly: WebJobsStartup(typeof(Startup))]

namespace OrderContext.Command.Function.Host
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder) => ConfigureServices(builder.Services);


        private void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);

            services.AddTransient<IOrderPolicy, OrderPolicy>();

            services.AddSingleton(_ => SystemClock.Now);

            ConfigureImGalaxyEs(services);

        }

        private void ConfigureImGalaxyEs(IServiceCollection services) =>
            services
                .AddImGalaxyESCosmosDBModule(configs =>
                {
                    configs.DatabaseId = "OrderContextES";
                    configs.SnapshotContainerName = "Snapshots";
                    configs.EndpointUri = "https://localhost:8081";
                    configs.PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                    configs.ReadBatchSize = 1000;
                    configs.IsSnapshottingOn = true;
                });
    }
}
