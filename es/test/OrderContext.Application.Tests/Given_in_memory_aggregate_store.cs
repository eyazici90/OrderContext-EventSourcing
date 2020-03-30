using ImGalaxy.ES.Core;
using ImGalaxy.ES.TestBase;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Integration.Tests; 
namespace OrderContext.Application.Tests
{
    public abstract class Given_in_memory_aggregate_store : GivenWhenThen
    {
        protected IAggregateStore AggregateStore { get; }
        public Given_in_memory_aggregate_store()
        {  
            AggregateStore = The<IAggregateStore>(); 
        }

        protected override IServiceCollection ConfigureServices(IServiceCollection services) =>
           OrderContextIntegrationConfigurator.Configure(services);
    }
}
