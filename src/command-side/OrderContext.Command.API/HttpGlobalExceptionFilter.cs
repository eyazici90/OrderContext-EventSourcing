using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderContext.Command.API
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            this._env = env ?? throw new ArgumentNullException(nameof(env));
        }
        public void OnException(ExceptionContext context)
        {
            context.Result = new OkObjectResult(context.Exception.Message);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            context.ExceptionHandled = true;
        }
    }
}
