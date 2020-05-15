using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http; 
using OrderContext.Query.Shared;
using OrderContext.Query.Shared.Models;
using System.Collections.Generic;
using System.Net; 
using System.Threading.Tasks;

namespace OrderContext.Query.Function.Host.Functions
{
    public class OrderQueryFunctions
    {
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Order))]
        [FunctionName("GetOrderById")]
        public async Task<IActionResult> GetOrderById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/Orders/{id}")] HttpRequest req,
            string id, 
            [CosmosDB( databaseName: SettingConsts.DATABASE, collectionName: SettingConsts.ORDER_CONTAINER,
                ConnectionStringSetting = "ConnString",
                Id = "{id}")
            ]Order state) =>
           new OkObjectResult(state);


        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Order[]))]
        [FunctionName("GetOrders")]
        public async Task<IActionResult> GetOrders(
          [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/Orders")] HttpRequest req,
          [CosmosDB( databaseName: SettingConsts.DATABASE, collectionName: SettingConsts.ORDER_CONTAINER,
                ConnectionStringSetting = "ConnString",
                SqlQuery = "SELECT * FROM Order")
            ]IEnumerable<Order> states) =>
         new OkObjectResult(states);


       
    }
}
