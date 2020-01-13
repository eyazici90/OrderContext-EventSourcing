using FluentAssertions; 
using MediatR; 
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using System; 
using System.Threading.Tasks;
using Xunit;
using static OrderContext.Domain.Messages.Orders.Commands;

namespace OrderContext.Integration.Tests
{
    public class Application_Tests : OrderContextIntegrationTestBase
    { 
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

            _fakeOrderId = new OrderId(await _mediatr.Send(command)); 
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
        public async Task ship_command_not_paid_order_should_throw()
        {
            var command = new ShipOrderCommand(_fakeOrderId);

            Func<Task> func = async () => await _mediatr.Send(command);

            func.Should().Throw<OrderNotPaidYetException>();
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
