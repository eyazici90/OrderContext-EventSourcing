using ImGalaxy.ES.Core;
using ImGalaxy.ES.ProtoActor;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders; 

namespace OrderContext.Application.Commands.Handlers
{
    public class OrderActor : CommandActor<OrderState>
    {
        private readonly IOrderPolicy _orderPolicy;
        public OrderActor(IAggregateStore aggregateStore,
            IOrderPolicy orderPolicy)
            : base(aggregateStore) 
        {
            _orderPolicy = orderPolicy;

            When<CreateOrderCommand>(cmd =>
             {
                 var order = Order.Create(new OrderId(cmd.Id), new CustomerId(cmd.BuyerId),
                         cmd.City, cmd.Street);

                 State = order.State;

                 return (cmd.Id, order);
             });

            When<PayOrderCommand>(cmd => cmd.OrderNumber, cmd => Order.PayOrder(State));

            When<ShipOrderCommand>(cmd => cmd.OrderNumber, cmd => Order.ShipOrder(State, _orderPolicy));

            When<CancelOrderCommand>(cmd => cmd.OrderNumber, cmd => Order.CancelOrder(State));
        }
    } 
}
