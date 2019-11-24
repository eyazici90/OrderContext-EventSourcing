using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using ImGalaxy.ES.CosmosDB.Modules;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Application.Infrastructure.Orders;
using OrderContext.Application.Validations;
using OrderContext.Domain.Orders;
using Swashbuckle.AspNetCore.Swagger;

namespace OrderContext.Command.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
         
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Order Command API", Version = "v1" });
            });

            services.AddOptions();

            services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);

            services.AddTransient<IOrderPolicy, OrderPolicy>();

            ConfigureImGalaxyEs(services);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddFluentValidation(fv => 
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {  
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Command API V1");
             });

            app.ApplicationServices
                .UseGalaxyESCosmosDBModule()
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });
        }

        private IServiceCollection ConfigureImGalaxyEs(IServiceCollection services) =>
            services
                .AddImGalaxyESCosmosDBModule(configs =>
                {
                    configs.DatabaseId = "OrderContextES";
                    configs.EventCollectionName = "Events";
                    configs.StreamCollectionName = "Streams";
                    configs.SnapshotCollectionName = "Snapshots";
                    configs.EndpointUri = ".";
                    configs.PrimaryKey = ".";
                    configs.ReadBatchSize = 1000;
                    configs.IsSnapshottingOn = true;
                    configs.OfferThroughput = 10000;
                });
    }
}
