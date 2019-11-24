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
        private readonly IOrderPolicy _orderPolicy;
        public ShipOrderCommandHandler(IUnitOfWork unitOfWork,
            IAggregateRootRepository<OrderState> rootRepository,
            IOrderPolicy orderPolicy)
            : base(unitOfWork, rootRepository) =>
            _orderPolicy = orderPolicy;


        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken) =>
            await UpdateAsync(new OrderId(request.OrderNumber), async state =>
                Order.ShipOrder(state, _orderPolicy)
            )
            .PipeToAsync(Unit.Value);

    }
}
