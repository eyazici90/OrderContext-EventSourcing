using FluentAssertions;
using ImGalaxy.ES.Core;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OrderContext.Application.Tests.Commands.CreateOrderCommand
{
    public class When_order_is_created : Given_in_memory_aggregate_store
    {
        private CustomerId _fakeBuyerId = CustomerId.New;
        private string _fakeOrderId;
        public When_order_is_created()
        {
            var command = new Domain.Messages.Orders.Commands.CreateOrderCommand(_fakeBuyerId, "Amsterdam", "Fake 12312");

            When(async () =>
            {
                _fakeOrderId = await new CreateOrderCommandHandler(AggregateStore).Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Then_it_should_be_added_to_aggregate_store()
        {
            var aggregate = await The<IAggregateStore>().Load<OrderState>(_fakeOrderId);

            (aggregate.Root as OrderState).OrderStatus.Should().Be(OrderStatus.Submitted);

        }

        [Fact]
        public async Task Then_its_status_should_be_submitted()
        {
            var aggregate = await The<IAggregateStore>().Load<OrderState>(_fakeOrderId);

            (aggregate.Root as OrderState).OrderStatus.Should().Be(OrderStatus.Submitted);

        }

    }

}
