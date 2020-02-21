using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using MediatR;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using static OrderContext.Domain.Messages.Orders.Commands;

namespace OrderContext.Command.Function.Host
{
    public class OrderCommandFunctions
    {
        private readonly IMediator _mediatr;
        public OrderCommandFunctions(IMediator mediatr) =>
            _mediatr = mediatr;
         
        [FunctionName("CreateOrderCommand")]
        public async Task<IActionResult> CreateOrder(
            [HttpTrigger(AuthorizationLevel.Function,  "post", Route = "v1/Orders")]
            [RequestBodyType(typeof(CreateOrderCommand), "create order")]HttpRequest req) =>
             await _mediatr.Send<CreateOrderCommand>(req);


        [FunctionName("PayOrderCommand")]
        public async Task<IActionResult> PayOrder(
            [HttpTrigger(AuthorizationLevel.Function,  "put", Route = "v1/Orders")]
            [RequestBodyType(typeof(PayOrderCommand), "pay order")]HttpRequest req) =>
             await _mediatr.Send<PayOrderCommand>(req);

    }
}
