using ImGalaxy.ES.Core;
using MediatR;
using OrderContext.Domain.Customers;
using OrderContext.Domain.Orders;
using OrderContext.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;
using static OrderContext.Domain.Messages.Orders.Commands;

namespace OrderContext.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler : AggregateCommandHandlerBase<OrderState, OrderId>,
        IRequestHandler<CreateOrderCommand, string>
    {
        private readonly Now _now;
        public CreateOrderCommandHandler(IAggregateStore aggregateStore,
            Now now)
            : base(aggregateStore) =>  _now = now;


        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newId = OrderId.New;
            return await AddAsync(async () => Order.Create(newId, new CustomerId(request.BuyerId),
                                                           request.City, request.Street, _now),  newId)
                 .PipeToAsync(newId);
        }
    }
}
