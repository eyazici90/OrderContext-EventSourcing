using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System; 
using System.Net; 

namespace OrderContext.Command.HttpApi.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public HttpGlobalExceptionFilter(IHostingEnvironment env) =>
            this._env = env ?? throw new ArgumentNullException(nameof(env));

        public void OnException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(context.Exception.Message);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.ExceptionHandled = true;
        }
    }
}
