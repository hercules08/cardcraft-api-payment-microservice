using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

namespace Cardcraft.Microservice.aCore
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IActionManager _manager;
        private readonly ILogger _logger;
        public ApiControllerBase(IActionManager manager, ILogger logger)
        {
            _manager = manager;
            _logger = logger;
        }
        public IActionManager ActionManager { get { return _manager; } }
        public ILogger Logger { get { return _logger; } }

        protected void LogException(Exception ex)
        {
            string errorMessage = LoggerHelper.GetExceptionDetails(ex);
            _logger.LogError(LOGGING_EVENTS.SERVICE_ERROR, ex, errorMessage);
            //HttpResponseMessage message = new HttpResponseMessage
            //{
            //    Content = new StringContent(errorMessage),
            //    StatusCode = System.Net.HttpStatusCode.ExpectationFailed
            //};
            //throw new HttpResponseException(message);
        }

        public string CONTEXT_USER
        {
            get { return User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value; }
        }
        public string CONTEXT_TOKEN
        {
            get { return HttpContext.Request.Headers["Authorization"][0].Remove(0, "Bearer ".Length); }
        }
    }
}