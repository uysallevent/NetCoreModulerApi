using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                try
                {
                    var requestBody = await FormatRequest(httpContext.Request);
                    await HandleExceptionAsync(httpContext, ex, requestBody);
                }
                catch (Exception aex)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(aex));
                    throw aex;
                }
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, string requestBody)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            Console.WriteLine(JsonConvert.SerializeObject(exception));
            return context.Response.WriteAsync(exception.Message);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var bodyStr = "";
            var req = request;
            req.EnableBuffering();

            using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
                req.Body.Position = 0;
            }

            return bodyStr;
        }
    }
}