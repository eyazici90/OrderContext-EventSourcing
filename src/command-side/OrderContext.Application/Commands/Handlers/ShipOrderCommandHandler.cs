using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderContext.Application.Commands.Handlers
{
    public class ShipOrderCommandHandler : CommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<ShipOrderCommand>
    {
        public ShipOrderCommandHandler(IUnitOfWork unitOfWork, 
            IAggregateRootRepository<OrderState> rootRepository) 
            : base(unitOfWork, rootRepository)
        {
        }

        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken) =>
            await UpdateAsync(new OrderId(request.OrderNumber), async state=> 
                Order.ShipOrder(state)
            )
            .PipeToAsync(Unit.Value);
         
    }
}
