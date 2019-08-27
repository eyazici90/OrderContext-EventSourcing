using MediatR;
using OrderContext.Query.API.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderContext.Query.API.Application.Queries.Handlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IList<Order>>
    {
        public async Task<IList<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken) =>
             ReadModelForOrderContext.Orders;
    }
}
