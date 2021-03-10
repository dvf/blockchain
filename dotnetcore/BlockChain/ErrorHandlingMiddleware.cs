using System;
using System.Net;
using System.Threading.Tasks;
using BlockChain.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BlockChain
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/problem+json";
                await context.Response.WriteAsync(new { Error = ex.Message }.AsJson());
            }
        }
    }
}