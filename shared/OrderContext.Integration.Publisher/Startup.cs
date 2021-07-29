using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Integration.Publisher.Application;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(OrderContext.Integration.Publisher.Startup))]

namespace OrderContext.Integration.Publisher
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IEventBus>(e => new NoEventBus());
        }
    }
}
