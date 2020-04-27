using ImGalaxy.ES.Core;
using ImGalaxy.ES.TestBase;
using OrderContext.Integration.Tests;

namespace OrderContext.Application.Tests
{
    public abstract class Given_in_memory_aggregate_store : GivenWhenThen
    {
        protected IAggregateStore AggregateStore { get; }
        public Given_in_memory_aggregate_store()
        {
            UseThe(services => OrderContextIntegrationConfigurator.Configure(services));

            AggregateStore = The<IAggregateStore>();
        }

    }
}
