using FluentAssertions;
using ImGalaxy.ES.Core; 
using OrderContext.Application.Commands.Handlers;
using OrderContext.Application.Tests.Scenarios.Given;
using OrderContext.Domain.Orders; 
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OrderContext.Application.Tests.Commands.CancelOrderCommand
{
    public class When_order_is_cancelled : Given_an_order_with_an_in_memory_aggregate_store
    {
        public When_order_is_cancelled(SeedDataFixture seedDataFixture) : base(seedDataFixture)
        {
            var command = new Domain.Messages.Orders.Commands.CancelOrderCommand(SeedDataFixture.FakeOrderId);

            When(async () =>
            { 
                await new CancelOrderCommandHandler(SeedDataFixture.AggregateStore).Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Then_its_status_should_be_changed()
        {
            var aggregate = await The<IAggregateStore>().Load<OrderState>(SeedDataFixture.FakeOrderId);

            (aggregate.Root as OrderState).OrderStatus.Should().Be(OrderStatus.Cancelled);
        } 
    } 
}
