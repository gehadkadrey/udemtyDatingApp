using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Errors;

namespace Api.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger
        , IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //if there not error continue
                await _next(context);
            }
            catch (Exception ex)
            {
                //if there is error
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType="application/json";
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

                //if in development  do first statement else do next
                var response=_env.IsDevelopment()
                 //use to display more details about the error in develpoment
                ?new ApiException(context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString())
               //display server error and not all details
                :new ApiException(context.Response.StatusCode,ex.Message,"internal server error");
                var option=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                var json=JsonSerializer.Serialize(response,option);
                await context.Response.WriteAsync(json);
         }
        }
    }
}