using MediatR;
using OrderContext.Query.API.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderContext.Query.API.Application.Queries
{
    public class GetOrderByIdQuery: IRequest<Order>
    {
        public readonly string OrderId;
        public GetOrderByIdQuery(string orderId)
        {
            OrderId = orderId;
        }
    }
}
