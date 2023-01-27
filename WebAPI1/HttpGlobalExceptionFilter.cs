using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Net;

namespace WebAPI1
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment webHostEnvironment, ILogger<HttpGlobalExceptionFilter> logger)
        {
            env= webHostEnvironment;
            _logger= logger;
        }

        //[NonAction]
        public void OnException(ExceptionContext context)
        {
      _logger.LogError(new EventId(context.Exception.HResult),context.Exception, context.Exception.Message);
            // This is often very handy information for tracing the specific request
                 var traceId = Activity.Current?.Id ?? context.HttpContext?.TraceIdentifier;

            var json = new JsonErrorResponse
            {
                Messages = new[] { "An error ocurred." },
                TraceId = traceId ?? string.Empty
            };

            if (env.EnvironmentName.Equals("Production"))//Development//Production
            {
                json.DeveloperMessage = context.Exception;
            }

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;       }
        private class JsonErrorResponse
        {
            public string[]? Messages { get; set; }
            public string? TraceId { get; set; }
            public object? DeveloperMessage { get; set; }
        }
    }
}
