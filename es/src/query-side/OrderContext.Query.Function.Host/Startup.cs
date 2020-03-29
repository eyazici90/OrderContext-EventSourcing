using ImGalaxy.ES.Projector;
using ImGalaxy.ES.Projector.CosmosDB.Modules;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Query.Function.Host;
using OrderContext.Query.Function.Host.Model;
using OrderContext.Query.Function.Host.Projections;
using System;

[assembly: WebJobsStartup(typeof(OrderContext.Projections.Startup))]

namespace OrderContext.Projections
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder) 
        { 
            ConfigureServices(builder.Services);
        }
          

        private void ConfigureServices(IServiceCollection services) =>
             services.UseProjectorForCosmosDb(
                opt =>
                {
                    opt.Database = SettingConsts.DATABASE;
                    opt.Collection = SettingConsts.ORDER_COLLECTION;
                    opt.EndpointUri = Environment.GetEnvironmentVariable("EndpointUri");
                    opt.PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
                },
                () =>  ProjectionDefiner
                            .From<Order>()
                            .To<OrderProjections>());

        }
}
