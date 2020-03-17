using ImGalaxy.ES.Projector;
using OrderContext.Query.Function.Host.Model;
using static OrderContext.Domain.Messages.Orders.Events;

namespace OrderContext.Query.Function.Host.Projections
{
    public class OrderProjections : Projection<Order>
    {
        public OrderProjections()
        {
            When<OrderStartedEvent>(async (e, state) =>
                 new Order
                 {
                     Id = e.OrderId,
                     OrderStatus = (int)OrderStatus.Submitted,
                     BuyerId = e.BuyerId,
                     Address = e.Street
                 }
            );

            When<OrderPaidEvent>(async (e, state) => state.OrderStatus = (int)OrderStatus.Paid);

            When<OrderShippedEvent>(async (e, state) => state.OrderStatus = (int)OrderStatus.Shipped);

            When<OrderCancelledEvent>(async (e, state) => state.OrderStatus = (int)OrderStatus.Cancelled);
        }
    }
}
