using ImGalaxy.ES.TestBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace OrderContext.Integration.Tests
{
    public abstract class OrderContextIntegratedTestBase : GivenWhenThen
    {
        protected override IServiceCollection ConfigureServices(IServiceCollection services) =>
           OrderContextIntegrationConfigurator.Configure(services);
       
    }
}
