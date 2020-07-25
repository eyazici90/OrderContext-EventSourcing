using Galaxy.Railway;
using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Domain.Orders;
using System.Threading;
using System.Threading.Tasks;
using static OrderContext.Domain.Messages.Orders.Commands;

namespace OrderContext.Application.Commands.Handlers
{
    public class ShipOrderCommandHandler : AggregateCommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<ShipOrderCommand>
    {
        private readonly IOrderPolicy _orderPolicy;
        public ShipOrderCommandHandler(IAggregateStore aggregateStore,
            IOrderPolicy orderPolicy) : base(aggregateStore) =>
            _orderPolicy = orderPolicy;

        public async Task<MediatR.Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken) =>
            await UpdateAsync(new OrderId(request.OrderNumber), async state => Order.ShipOrder(state, _orderPolicy))
                  .MapAsync(_ => MediatR.Unit.Value);

    }
}
