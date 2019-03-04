using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace Cardcraft.Microservice.aCore
{
    public interface IActionManager
    {

    }

    public class ActionManagerBase:IActionManager
    {
        private readonly ILogger _logger;

        public ActionManagerBase(ILogger logger)
        {
            _logger = logger;
        }

        public ILogger Logger { get { return _logger; } }

        public void LogException(Exception ex, object model = null, int errorCode = LOGGING_EVENTS.ERROR)
        {
            string serializedObject = string.Empty;
            string exceptionDetails = string.Empty;
            string errorMessage = string.Empty;

            if (model != null)
            {
                try
                {
                    serializedObject = JsonConvert.SerializeObject(model, Formatting.Indented);
                }
                catch
                { }
            }

            exceptionDetails = LoggerHelper.GetExceptionDetails(ex);

            if (!string.IsNullOrWhiteSpace(serializedObject))
                errorMessage = $"{exceptionDetails}\n{serializedObject}";
            else
                errorMessage = exceptionDetails;

            _logger.LogError(errorCode, errorMessage);
        }
    }
}
