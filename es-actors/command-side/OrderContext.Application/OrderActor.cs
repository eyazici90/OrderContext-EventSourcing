using ImGalaxy.ES.Core;
using ImGalaxy.ES.ProtoActor;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using OrderContext.Domain.Shared;
using static OrderContext.Domain.Messages.Orders.Commands;

namespace OrderContext.Application.Commands.Handlers
{
    public class OrderActor : CommandActor<OrderState>
    {
        public OrderActor(IAggregateStore aggregateStore,
            IOrderPolicy orderPolicy,
            Now now)
            : base(aggregateStore)
        {
            When<CreateOrderCommand>(cmd =>
             {
                 var order = Order.Create(new OrderId(ActorId),
                                          new CustomerId(cmd.BuyerId),
                                             cmd.City,
                                             cmd.Street,
                         now);

                 State = order.State;

                 return (ActorId, order);
             });

            When<PayOrderCommand>(cmd => cmd.OrderNumber, cmd => Order.PayOrder(State));

            When<ShipOrderCommand>(cmd => cmd.OrderNumber, cmd => Order.ShipOrder(State, orderPolicy));

            When<CancelOrderCommand>(cmd => cmd.OrderNumber, cmd => Order.CancelOrder(State));
        }
    }
}
