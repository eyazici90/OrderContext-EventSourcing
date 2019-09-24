using FluentAssertions;
using ImGalaxy.ES.TestBase;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderContext.Application.Commands;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderContext.Integration.Tests
{
    public class Application_Tests : ImGalaxyIntegrationTestBase
    {
        protected override IServiceCollection ConfigureServices(IServiceCollection services) =>
          OrderContextIntegrationConfigurator.Configure(services);

        private OrderId _fakeOrderId;
        private readonly IMediator _mediatr;
        public Application_Tests()
        {
            _mediatr = GetRequiredService<IMediator>(); 

            SeedOrder().ConfigureAwait(false)
                .GetAwaiter().GetResult();
        }

        private async Task SeedOrder()
        {
            var fakeBuyerId = CustomerId.New;

            var command = new CreateOrderCommand(fakeBuyerId, "Amsterdam", "Fake Street-1");

            _fakeOrderId = await _mediatr.Send(command); 
        }


        [Fact]

        public async Task create_order_command_with_valid_command_should_success()
        {  
            var fakeCity = "Istanbul";

            var command = new CreateOrderCommand(CustomerId.New, fakeCity, "Fake-1");

            var result = await _mediatr.Send(command);

            result.Should().NotBeNull();
        }


        [Fact]
        public async Task pay_order_command_with_valid_command_should_success()
        {
            var command = new PayOrderCommand(_fakeOrderId);

            var result = await _mediatr.Send(command);

            result.Should().Be(Unit.Value); 
        }


        [Fact]
        public async Task cancel_order_command_with_valid_command_should_success()
        {
            var command = new CancelOrderCommand(_fakeOrderId);

            var result = await _mediatr.Send(command);

            result.Should().Be(Unit.Value);
        }
  

    }
}
