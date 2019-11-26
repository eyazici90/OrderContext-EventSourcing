using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System; 
using System.Net.Http; 

[assembly: FunctionsStartup(typeof(OrderContext.Projections.Startup))]

namespace OrderContext.Projections
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(s =>
                new OrderContextQueryClient(Environment.GetEnvironmentVariable("OrderContext_Api"), HttpClientFactory.Create())
            );
        }
    }
}
