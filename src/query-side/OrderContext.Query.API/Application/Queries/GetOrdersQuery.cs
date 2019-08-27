using MediatR;
using OrderContext.Query.API.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderContext.Query.API.Application.Queries
{
    public class GetOrdersQuery: IRequest<IList<Order>>
    {
    }
}
