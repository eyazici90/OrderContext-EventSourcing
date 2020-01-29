using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; 
using System.IO; 
using System.Threading.Tasks;

namespace OrderContext.Command.Function.Host
{
    public abstract class CommandFunctionBase
    {
        protected IMediator Mediatr { get; }
        public CommandFunctionBase(IMediator mediatr) =>
            Mediatr = mediatr;

        protected virtual async Task<IActionResult> Handle<TCommand>(HttpRequest req)  where TCommand : class
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);

            var command = JsonConvert.DeserializeObject<TCommand>(requestBody);

            var result = await Mediatr.Send(command).ConfigureAwait(false);

            return new OkObjectResult(result);

        }
    }
}
