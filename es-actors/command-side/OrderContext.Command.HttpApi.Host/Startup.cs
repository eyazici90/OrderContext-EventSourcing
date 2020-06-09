using FluentValidation.AspNetCore; 
using ImGalaxy.ES.ProtoActor; 
using MediatR;
using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Application.Validations;
using OrderContext.Command.HttpApi.Filters;
using OrderContext.Domain.Orders;
using OrderContext.Domain.Shared;
using Proto; 
using static OrderContext.Domain.Messages.Orders.Commands;

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
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Command API", Version = "v1" });
                }).AddSwaggerGenNewtonsoftSupport();

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

            services
                .AddControllers(opt => opt.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                .AddNewtonsoftJson()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddSingleton(_ => SystemClock.Now);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Command API V1");
             });
        }

        private IServiceCollection ConfigureImGalaxyEs(IServiceCollection services) =>
            services
                .AddImGalaxyESCosmosDBModule(configs =>
                {
                    configs.DatabaseId = "OrderContextES";  
                    configs.EndpointUri = "https://localhost:8081";
                    configs.StreamCollectionName = "Streams";
                    configs.EventCollectionName = "Events";
                    configs.PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                    configs.ReadBatchSize = 1000;  
                });
    }
}
