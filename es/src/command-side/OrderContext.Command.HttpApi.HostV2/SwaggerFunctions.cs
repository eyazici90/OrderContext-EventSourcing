using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http; 
using System.Net.Http; 
using System.Threading.Tasks;

namespace OrderContext.Command.HttpApi.HostV2
{
    public static class SwaggerFunctions
    {
        [SwaggerIgnore]
        [FunctionName("Swagger")]
        public static Task<HttpResponseMessage> Swagger(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/json")]
            HttpRequestMessage req,
            [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerDocumentResponse(req));
        }

        [SwaggerIgnore]
        [FunctionName("SwaggerUi")]
        public static Task<HttpResponseMessage> SwaggerUi(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/ui")]
            HttpRequestMessage req,
            [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json"));
        }
    }
}
