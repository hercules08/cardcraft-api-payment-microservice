using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Cardcraft.Microservice.aCore
{
    public class GlobalExceptionFilter:IExceptionFilter
    {
        private readonly string _defaultErrorMessage;
        private ILogger <GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IConfiguration configuration, ILogger<GlobalExceptionFilter> logger)
        {
            _defaultErrorMessage = configuration[KEY.DEFAULT_ERROR_MESSAGE];
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = _defaultErrorMessage ?? "Sorry we are experiencing technical difficulties. Please try again later.";

            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                _logger.LogError(context.Exception, "Unauthorized Access");
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                _logger.LogError(context.Exception, "A server error occurred.");
                status = HttpStatusCode.NotImplemented;
            }
            else
            {
                _logger.LogError(context.Exception, LoggerHelper.GetExceptionDetails(context.Exception));
                status = HttpStatusCode.BadRequest;
            }
            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            //var err = message + " " + context.Exception.StackTrace;
            response.WriteAsync(message);
        }
    }
}
