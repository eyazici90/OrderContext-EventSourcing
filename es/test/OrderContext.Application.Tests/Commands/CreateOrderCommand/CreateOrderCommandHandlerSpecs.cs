using FluentAssertions;
using ImGalaxy.ES.Core;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Domain.Customers;
using OrderContext.Integration.Tests;
using System.Threading;
using Xunit;

namespace OrderContext.Application.Tests.Commands.CreateOrderCommand
{
    public class When_create_order_is_handled : Given_in_memory_aggregate_store
    {
        private CustomerId _fakeBuyerId = CustomerId.New;
        private string _result;
        public When_create_order_is_handled()
        {
            When(async () =>
            {
                var command = new Domain.Messages.Orders.Commands.CreateOrderCommand(_fakeBuyerId, "Amsterdam", "Fake 12312");

                _result = await new CreateOrderCommandHandler(AggregateStore).Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public void Then_command_should_succeed()
        {
            _result.Should().NotBeNullOrEmpty();
        }

    }
    public class Given_in_memory_aggregate_store : OrderContextIntegrationTestBase
    {
        protected IAggregateStore AggregateStore { get; }
        public Given_in_memory_aggregate_store()
        {
            AggregateStore = The<IAggregateStore>();
        }
    }
}
