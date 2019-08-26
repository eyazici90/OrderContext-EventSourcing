using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderContext.Application.Commands;

namespace OrderContext.Command.API.Controllers
{
    [Route("api/v1/Orders")]
    [ApiController]
    public class OrderCommandsController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public OrderCommandsController(IMediator mediatr) =>
            _mediatr = mediatr;
         
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task CreateOrder([FromBody]CreateOrderCommand command) =>
           await _mediatr.Send(command);

        [Route("cancel")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task CancelOrder([FromBody]CancelOrderCommand command) =>
            await _mediatr.Send(command);

        [Route("ship")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task ShipOrder([FromBody]ShipOrderCommand command) =>
          await _mediatr.Send(command);

        [Route("pay")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task PayOrder([FromBody]PayOrderCommand command) =>
            await _mediatr.Send(command);

    }
}