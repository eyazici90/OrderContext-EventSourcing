using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Projections;
using OrderContext.Projections.Connectors;
using OrderContext.Projections.Projections;
using System; 


[assembly: WebJobsStartup(typeof(Startup))]

namespace OrderContext.Projections
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            ConfigureServices(builder.Services);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new CosmosClientBuilder(Environment.GetEnvironmentVariable("ConnectionString")).Build());
            services.AddSingleton<ICosmosDbConnector, CosmosDbConnector>();
            services.AddProjector<ICosmosDbConnector>(typeof(OrderProjections));
        }
    }
}