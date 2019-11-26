using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderContext.Domain.Messages.Orders;
using OrderContext.Query.API.Application;
using OrderContext.Query.API.Application.Model;

namespace OrderContext.Query.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/Projections")]
    [ApiController]
    public class ProjectionHandlersController : ControllerBase
    {

        [HttpPost]
        [Route("order-started")]
        public async Task When([FromBody]OrderStartedEvent @event) =>
              ReadModelForOrderContext.locker.Lock(()=> 
              {
                  ReadModelForOrderContext.Orders.Add(new Order
                  {
                       BuyerId = @event.BuyerId,
                       City = @event.City,
                       Id = @event.OrderId,
                       Street = @event.Street,
                       OrderStatus = "Started"
                  });
              });

        [HttpPut]
        [Route("order-paid")]
        public async Task When([FromBody]OrderPaidEvent @event)=>
              ReadModelForOrderContext.locker.Lock(()=> 
              {
                   var existingOrder = ReadModelForOrderContext.Orders.SingleOrDefault(o=>o.Id == @event.OrderId);
                   existingOrder.OrderStatus = "Paid";
              });

        [HttpPut]
        [Route("order-cancelled")]
        public async Task When([FromBody]OrderCancelledEvent @event) =>
              ReadModelForOrderContext.locker.Lock(() =>
              {
                  var existingOrder = ReadModelForOrderContext.Orders.SingleOrDefault(o => o.Id == @event.OrderId);
                  existingOrder.OrderStatus = "Cancelled";
              });

        [HttpPut]
        [Route("order-shipped")]
        public async Task When([FromBody]OrderShippedEvent @event) =>
              ReadModelForOrderContext.locker.Lock(() =>
              {
                  var existingOrder = ReadModelForOrderContext.Orders.SingleOrDefault(o => o.Id == @event.OrderId);
                  existingOrder.OrderStatus = "Shipped";
              });
    }
}