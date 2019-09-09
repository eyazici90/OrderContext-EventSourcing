using ImGalaxy.ES.InMemory.Modules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Application.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderContext.Integration.Tests
{
    public static class OrderContextIntegrationConfigurator
    {
        public static IServiceCollection Configure(IServiceCollection services) =>
            services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly)
                    .AddImGalaxyESInMemoryModule();
    }
}
