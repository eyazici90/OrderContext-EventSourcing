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
    public class PayOrderCommandHandler : CommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<PayOrderCommand>
    {
        public PayOrderCommandHandler(IUnitOfWork unitOfWork,
            IAggregateRootRepository<OrderState> rootRepository) 
            : base(unitOfWork, rootRepository)
        {
        }

        public async Task<Unit> Handle(PayOrderCommand request, CancellationToken cancellationToken)=>
           await UpdateAsync(new OrderId(request.OrderNumber), async state =>
               Order.PayOrder(state)
           )
           .PipeToAsync(Unit.Value);
    }
}
