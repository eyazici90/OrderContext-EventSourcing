using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using OrderContext.Query.Function.Host.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderContext.Query.Function.Host.Functions
{
    public class OrderQueryFunctions
    {
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Order))]
        [FunctionName("GetOrderById")]
        public async Task<IActionResult> GetOrderById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/Measurements/{id}")] HttpRequest req,
            string id,
            [SwaggerIgnore]
          [CosmosDB( databaseName: SettingConsts.DATABASE, collectionName: SettingConsts.ORDER_COLLECTION,
                ConnectionStringSetting = "ConnString",
                Id = "{id}")
            ]Order state) =>
           new OkObjectResult(state);


        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Order[]))]
        [FunctionName("GetOrders")]
        public async Task<IActionResult> GetOrders(
          [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/Orders")] HttpRequest req,
          [SwaggerIgnore]
          [CosmosDB( databaseName: SettingConsts.DATABASE, collectionName: SettingConsts.ORDER_COLLECTION,
                ConnectionStringSetting = "ConnString",
                SqlQuery = "SELECT * FROM Order")
            ]IEnumerable<Order> states) =>
         new OkObjectResult(states);


       
    }
}
