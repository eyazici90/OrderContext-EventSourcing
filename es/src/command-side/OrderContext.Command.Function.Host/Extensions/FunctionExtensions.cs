using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; 
using System.IO; 
using System.Threading.Tasks;

namespace OrderContext.Command.Function.Host
{
    public static class FunctionExtensions
    {  
        public static async Task<IActionResult> Send<TCommand>(this IMediator mediator,
            HttpRequest httpRequest)
        {
            var requestBody = await new StreamReader(httpRequest.Body).ReadToEndAsync().ConfigureAwait(false);

            var command = JsonConvert.DeserializeObject<TCommand>(requestBody);

            var result = await mediator.Send(command).ConfigureAwait(false);

            return new OkObjectResult(result); 
        } 
    }
}
