using Galaxy.Railway;
using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Domain.Orders;
using System.Threading;
using System.Threading.Tasks;
using static OrderContext.Domain.Messages.Orders.Commands;
using Unit = MediatR.Unit;

namespace OrderContext.Application.Commands.Handlers
{
    public class CancelOrderCommandHandler : AggregateCommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<CancelOrderCommand>
    {
        public CancelOrderCommandHandler(IAggregateStore aggregateStore)
            : base(aggregateStore)
        {
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken) =>
            await UpdateAsync(new OrderId(request.OrderNumber), async state => Order.CancelOrder(state))
                    .MapAsync(_ => Unit.Value);
    }
}
