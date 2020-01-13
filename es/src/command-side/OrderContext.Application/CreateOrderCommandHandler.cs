using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders; 
using System.Threading;
using System.Threading.Tasks;
using static OrderContext.Domain.Messages.Orders.Commands;

namespace OrderContext.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler : CommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<CreateOrderCommand, string>
    {
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, 
            IAggregateRootRepository<OrderState> rootRepository) 
            : base(unitOfWork, rootRepository)
        {
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newId = OrderId.New;
            return await AddAsync(async () => Order.Create(newId, new CustomerId(request.BuyerId), request.City, request.Street)
                                           .State,
                                           newId)
                 .PipeToAsync(newId);
        } 
    }
}
