using ImGalaxy.ES.TestBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace OrderContext.Integration.Tests
{
    public abstract class OrderContextIntegrationTestBase : ImGalaxyIntegrationTestBase
    {
        protected override IServiceCollection ConfigureServices(IServiceCollection services) =>
           OrderContextIntegrationConfigurator.Configure(services);
         
        protected virtual void When(Func<Task> whenFunc) =>
            whenFunc()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        protected virtual void When<TResult>(Func<Task<TResult>> whenFunc) =>
            whenFunc()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

        protected virtual void When<TCommand>(TCommand cmd, Func<TCommand, Task> whenFunc) =>
               whenFunc(cmd)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
    }
}
