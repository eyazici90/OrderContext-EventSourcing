using FluentValidation.AspNetCore;
using ImGalaxy.ES.Core;
using ImGalaxy.ES.CosmosDB;
using ImGalaxy.ES.CosmosDB.Modules;
using ImGalaxy.ES.ProtoActor;
using ImGalaxy.ES.ProtoActor.Modules;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Application;
using OrderContext.Application.Commands;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Application.Infrastructure.Orders;
using OrderContext.Application.Validations;
using OrderContext.Domain.Orders;
using Proto;
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

            services.AddMediatR(typeof(CreateOrderCommand).Assembly);

            services.AddProtoActor(props =>
            {
                props.RegisterProps<OrderActor>(p =>
                    p.WithReceiveMiddleware(next => ActorMiddleware.Exception(next, nameof(OrderActor))));
            });

            ConfigureImGalaxyEs(services)
                .AddImGalaxyESProtoActorModule();

            services.AddTransient<IOrderPolicy, OrderPolicy>();

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
                    configs.EndpointUri = "https://localhost:8081";
                    configs.PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                    configs.ReadBatchSize = 1000;
                    configs.IsSnapshottingOn = true;
                    configs.OfferThroughput = 10000;
                });
    }
}
