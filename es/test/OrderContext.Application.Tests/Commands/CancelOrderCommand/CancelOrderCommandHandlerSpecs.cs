using FluentAssertions;
using MediatR;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Integration.Tests;
using System.Threading;
using Xunit;

namespace OrderContext.Application.Tests.Commands.CancelOrderCommand
{
    public class When_cancel_order_is_handled : Given_in_memory_aggregate_store
    {
        private Unit _result;
        public When_cancel_order_is_handled(SeedDataFixture seedDataFixture) : base(seedDataFixture)
        {
            When(async () =>
            {
                var command = new Domain.Messages.Orders.Commands.CancelOrderCommand(SeedDataFixture.FakeOrderId);

                _result = await new CancelOrderCommandHandler(SeedDataFixture.AggregateStore).Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public void Then_command_should_be_succeed() =>
            _result.Should().Be(Unit.Value);

    }

    public class Given_in_memory_aggregate_store : OrderContextIntegrationTestBase,
        IClassFixture<SeedDataFixture>
    {
        protected SeedDataFixture SeedDataFixture { get; }
        public Given_in_memory_aggregate_store(SeedDataFixture seedDataFixture) =>
            SeedDataFixture = seedDataFixture;
    }
}
