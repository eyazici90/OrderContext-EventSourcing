using ImGalaxy.ES.Projector;
using ImGalaxy.ES.TestBase;
using OrderContext.Projections.Connectors;
using OrderContext.Projections.Projections;
using OrderContext.Projections.Tests.Stubs;
using System.Collections.Generic;

namespace OrderContext.Projections.Tests.Scenarios.Given
{
    public class Given_in_memory_connector : GivenWhenThen
    {
        public Given_in_memory_connector()
        {
            var connector = new InMemoryConnector();

            UseThe<ICosmosDbConnector>(connector);

            UseThe<IProjector>(() =>
                 new ConnectedProjector<ICosmosDbConnector>(connector, _ => new List<object> { new OrderProjections() })
            );

            EnsureContainerInitialized();
        }
    }
}
