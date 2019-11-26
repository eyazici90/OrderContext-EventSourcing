using System;
using System.Net;
using System.Threading.Tasks;
using ImGalaxy.ES.ProtoActor;
using Microsoft.AspNetCore.Mvc;
using OrderContext.Application.Commands;
using OrderContext.Application.Commands.Handlers;
using OrderContext.Domain.Orders;

namespace OrderContext.Command.API.Controllers
{
    [Route("api/v1/Orders")]
    [ApiController]
    public class OrderCommandsController : ControllerBase
    {
        private readonly IActorManager _actorManager;
        public OrderCommandsController(IActorManager actorManager) =>
            _actorManager = actorManager;

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task CreateOrder([FromBody]CreateOrderCommand command) =>
            await Handle<CreateOrderCommand, OrderActor>(cmd => cmd.Id, command);
         
        [Route("cancel")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task CancelOrder([FromBody]CancelOrderCommand command) =>
             await Handle<CancelOrderCommand, OrderActor>(cmd => cmd.OrderNumber, command);

        [Route("ship")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task ShipOrder([FromBody]ShipOrderCommand command) =>
             await Handle<ShipOrderCommand, OrderActor>(cmd => cmd.OrderNumber, command);

        [Route("pay")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task PayOrder([FromBody]PayOrderCommand command) =>
             await Handle<PayOrderCommand, OrderActor>(cmd => cmd.OrderNumber, command);
         

        private async Task Handle<TCommand, TActor>(Func<TCommand, string> func, TCommand command)
        {
            var actorId = func(command);

            var result = await _actorManager.RequestAsync<object, OrderActor>(actorId, command);

            if (result is ExceptionOccuredDuringHandleEvent ex)
                throw ex.Exception;

        }

    }
}