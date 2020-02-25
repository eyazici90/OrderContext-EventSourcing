using ImGalaxy.ES.TestBase;
using Microsoft.Extensions.DependencyInjection; 

namespace OrderContext.Integration.Tests
{
    public abstract class OrderContextIntegrationTestBase : ImGalaxyIntegrationTestBase
    {
        protected override IServiceCollection ConfigureServices(IServiceCollection services) =>
           OrderContextIntegrationConfigurator.Configure(services);
    }
}
