using ImGalaxy.ES.InMemory.Modules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Application.Commands.Handlers; 
using OrderContext.Domain.Orders; 

namespace OrderContext.Integration.Tests
{
    public static class OrderContextIntegrationConfigurator
    {
        public static IServiceCollection Configure(IServiceCollection services) =>
            services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly)
                    .AddTransient<IOrderPolicy, OrderPolicy>()
                    .AddImGalaxyESInMemoryModule();
    }
}
