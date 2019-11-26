using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderContext.Query.API.Application.Model;
using OrderContext.Query.API.Application.Queries;

namespace OrderContext.Query.API.Controllers
{
    [Route("api/v1/Orders")]
    [ApiController]
    public class OrderQueriesController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public OrderQueriesController(IMediator mediatr) =>
            _mediatr = mediatr;


        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IList<Order>> GetOrders() =>
            await _mediatr.Send(new GetOrdersQuery());


        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<Order> GetOrderById([FromRoute]string id) =>
            await _mediatr.Send(new GetOrderByIdQuery(id));

    }
}