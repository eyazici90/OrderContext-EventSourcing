using FluentAssertions;
using ImGalaxy.ES.Projector;
using OrderContext.Projections.Connectors;
using OrderContext.Projections.Tests.Scenarios.Given;
using OrderContext.Query.Shared.Models;
using System;
using System.Threading.Tasks;
using Xunit;
using static OrderContext.Domain.Messages.Orders.Events;

namespace OrderContext.Projections.Tests.Scenarios
{
    public class When_event_is_projected : Given_in_memory_connector
    {
        private const string ORDERID = "543";
        public When_event_is_projected()
        {
            var @event = new OrderStartedEvent(ORDERID, "123", "Amsterdam", "Fake-1", DateTime.UtcNow);

            When(async () =>
            {
                await The<IProjector>().ProjectAsync(@event).ConfigureAwait(false);
            });
        }

        [Fact]
        public async Task Then_it_should_be_projected()
        {
            var state = await The<ICosmosDbConnector>().ReadItemAsync<Order>(ORDERID).ConfigureAwait(false);

            state.Should().NotBeNull();

            state.Id.Should().Be(ORDERID);

            state.OrderStatus.Should().Be((int)OrderStatus.Submitted);
        }
    }
}
