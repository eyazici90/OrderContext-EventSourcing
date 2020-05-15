using ImGalaxy.ES.Projector;
using OrderContext.Projections.Connectors;
using OrderContext.Query.Shared.Models;
using static OrderContext.Domain.Messages.Orders.Events;

namespace OrderContext.Projections.Projections
{
    public class OrderProjections : Projection<ICosmosDbConnector>
    {
        public OrderProjections()
        {
            When<OrderStartedEvent>(async (@event, connector) =>
            {
                await connector.CreateAsync(new Order
                {
                    Id = @event.OrderId,
                    OrderStatus = (int)OrderStatus.Submitted,
                    BuyerId = @event.BuyerId,
                    Address = @event.Street
                }).ConfigureAwait(false);
            });

            When<OrderPaidEvent>(async (@event, connector) =>
            {
                var state = await connector.ReadItemAsync<Order>(@event.OrderId).ConfigureAwait(false);

                state.OrderStatus = (int)OrderStatus.Paid;

                await connector.ReplaceAsync(state, @event.OrderId).ConfigureAwait(false);
            });

            When<OrderShippedEvent>(async (@event, connector) =>
            {
                var state = await connector.ReadItemAsync<Order>(@event.OrderId).ConfigureAwait(false);

                state.OrderStatus = (int)OrderStatus.Shipped;

                await connector.ReplaceAsync(state, @event.OrderId).ConfigureAwait(false);
            });

            When<OrderCancelledEvent>(async (@event, connector) =>
            {
                var state = await connector.ReadItemAsync<Order>(@event.OrderId).ConfigureAwait(false);

                state.OrderStatus = (int)OrderStatus.Cancelled;

                await connector.ReplaceAsync(state, @event.OrderId).ConfigureAwait(false);
            });
        } 
    }
}
