using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RESTbottle.Filters
{
    /// <summary>
    /// Action filter that allows only GET requests (and OPTIONS/HEAD to avoid breaking CORS/preflight).
    /// Returns 405 Method Not Allowed for other HTTP methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class OnlyGetAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            // Allow GET and HEAD (safe/read-only) and OPTIONS to support CORS preflight.
            if (HttpMethods.IsGet(method) || HttpMethods.IsHead(method) || HttpMethods.IsOptions(method))
            {
                await next();
                return;
            }

            context.Result = new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
        }
    }
}
