using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderContext.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler : CommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<CreateOrderCommand>
    {
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, 
            IAggregateRootRepository<OrderState> rootRepository) 
            : base(unitOfWork, rootRepository)
        {
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrderId = OrderId.New;
            return  await AddAsync(async () => Order.Create(newOrderId, new CustomerId(request.BuyerId), request.City, request.Street)
                                            .State,
                                            newOrderId)
                  .PipeToAsync(Unit.Value);
        }
       
    }
}
