using MediatR;
using OrderContext.Query.API.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderContext.Query.API.Application.Queries.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken) =>
            ReadModelForOrderContext.Orders.SingleOrDefault(o=> o.Id == request.OrderId);
    }
}
