using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Domain.Orders; 
using System.Threading;
using System.Threading.Tasks;
using static OrderContext.Domain.Messages.Orders.Commands;

namespace OrderContext.Application.Commands.Handlers
{
    public class PayOrderCommandHandler : AggregateCommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<PayOrderCommand>
    {
        public PayOrderCommandHandler(IAggregateStore aggregateStore) 
            : base(aggregateStore)
        {
        }

        public async Task<Unit> Handle(PayOrderCommand request, CancellationToken cancellationToken)=>
           await UpdateAsync(new OrderId(request.OrderNumber), async state =>  Order.PayOrder(state))
                  .PipeToAsync(Unit.Value);
    }
}
