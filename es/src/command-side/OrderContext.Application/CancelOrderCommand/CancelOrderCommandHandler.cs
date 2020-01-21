using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Domain.Orders; 
using System.Threading;
using System.Threading.Tasks;
using static OrderContext.Domain.Messages.Orders.Commands;

namespace OrderContext.Application.Commands.Handlers
{
    public class CancelOrderCommandHandler : CommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<CancelOrderCommand>
    {
        public CancelOrderCommandHandler(IUnitOfWork unitOfWork,
            IAggregateRootRepository<OrderState> rootRepository) 
            : base(unitOfWork, rootRepository)
        {
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken) =>
            await UpdateAsync(new OrderId(request.OrderNumber), async state=> 
                Order.CancelOrder(state)
            )
            .PipeToAsync(Unit.Value);
    }
}
